using Microsoft.Extensions.Logging;
using WhoDeDoVille.ReactionTester.Logging;

namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Repository;

//TODO: Add testing
public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : BaseEntity
{
    ///// <summary>
    ///// Container name used when creating container
    ///// </summary>
    //public abstract string ContainerName { get; }

    ///// <summary>
    ///// Container name used when creating container
    ///// </summary>
    //public abstract string PartitionKeyPath { get; }

    ///// <summary>
    ///// Container is initialized.
    ///// </summary>
    //public abstract bool IsInitialized { get; }

    /// <summary>
    /// Generate id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract string GenerateId(T entity);

    /// <summary>
    /// Resolve the partition key
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public abstract PartitionKey ResolvePartitionKey(string entityId);

    /// <summary>
    /// Used to tell the SDK whether or not to try and creates databases and containers if they do not exist.
    /// </summary>
    /// <remarks>This feature is very powerful for local development. However, in scenarios where infrastructure as code is used this may not be required.</remarks>
    public bool IsAutoResourceCreationIfNotExistsEnabled { get; set; } = true;

    public abstract ContainerProperties GenerateContainerProperties();

    /// <summary>
    /// Cosmos DB factory
    /// </summary>
    private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;

    private IContainerInfoEntity _containerSettingsInfo;
    private IDatabaseInfoEntity _databaseInfo;
    private readonly LoggingCosmosExceptionMessages _loggingCosmosExceptionMessages;

    /// <summary>
    /// Cosmos DB database
    /// </summary>
    private readonly Database _database;

    /// <summary>
    /// Cosmos DB container
    /// </summary>
    private readonly Container _container;

    public CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory,
        ILogger logger, IContainerInfoEntity containerSettingsInfo)
    {
        _loggingCosmosExceptionMessages = new LoggingCosmosExceptionMessages(logger);
        _containerSettingsInfo = containerSettingsInfo;
        _databaseInfo = cosmosDbContainerFactory.GetDatabaseInfo();

        _cosmosDbContainerFactory = cosmosDbContainerFactory
            ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
        _database = _cosmosDbContainerFactory.GetDatabase()._database;
        if (_containerSettingsInfo.IsInitialized == false)
        {
            _containerSettingsInfo.IsInitialized = true;
            GenerateContainer();
        }
        _container = _cosmosDbContainerFactory.GetContainer(_containerSettingsInfo.ContainerName)._container;
    }

    /// <summary>
    /// Adds single item to container.
    /// Checks if Id has a value and Generates an Id if does not.
    /// Resolves partition key.
    /// </summary>
    /// <param name="item"></param>
    public async Task AddItemAsync(T item)
    {
        item.Id ??= GenerateId(item);

        //if (item.Id == null)
        //{
        //    item.Id = GenerateId(item);
        //}

        var key = ResolvePartitionKey(item.Id);
        await _container.CreateItemAsync(item, key);
    }

    /// <summary>
    /// Adds List if items to container.
    /// Checks if Id has a value and Generates an Id if does not.
    /// Resolves partition key.
    /// </summary>
    /// <param name="item"></param>
    public async Task AddItemsAsync(List<T> items)
    {
        List<Task> tasks = new List<Task>();
        foreach (T item in items)
        {
            if (item.Id == null)
            {
                item.Id = GenerateId(item);
            }

            var key = ResolvePartitionKey(item.Id);

            tasks.Add(_container.CreateItemAsync(item, key)
                .ContinueWith(itemResponse =>
                {
                    if (!itemResponse.IsCompletedSuccessfully)
                    {
                        AggregateException innerExceptions = itemResponse.Exception.Flatten();
                        if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                        {
                            Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                        }
                        else
                        {
                            Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                        }
                    }
                }));
        }

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Deletes single item from container by Id.
    /// Resolves partition key.
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
    }

    /// <summary>
    /// Delete container.
    /// </summary>
    /// <returns></returns>
    public async Task DeleteContainer()
    {
        await _container.DeleteContainerAsync();
    }

    /// <summary>
    /// Gets single item from container by Id.
    /// Resolves partition key.
    /// </summary>
    /// <param name="id"></param>
    public async Task<T> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets many items from container by Id and partition key.
    /// Resolves partition key.
    /// </summary>
    /// <param name="id"></param>
    public async Task<IEnumerable<T>> GetItemsByIdAndPartitionKeyAsync(List<(string, PartitionKey)> itemsToFind)
    {
        // TODO: Needs to be reworked to just use a list of Ids if possible
        List<T> results = new();
        try
        {
            FeedResponse<T> feedResponse = await _container.ReadManyItemsAsync<T>(
                items: itemsToFind,
                readManyRequestOptions: new ReadManyRequestOptions()
                {
                    Properties = new Dictionary<string, object>()
                }
            );

            results.AddRange(feedResponse.ToList());
        }
        catch (Exception ex)
        {

        }
        return results;
    }

    /// <summary>
    /// Gets items from container by query.
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns>IEnumerable with results</returns>
    public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
    {
        FeedIterator<T> resultSetIterator = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
        List<T> results = new();
        while (resultSetIterator.HasMoreResults)
        {
            FeedResponse<T> response = await resultSetIterator.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }

    /// <summary>
    /// Updates item in container
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    public async Task UpdateItemAsync(string id, T item)
    {
        // Update
        await _container.UpsertItemAsync<T>(item, ResolvePartitionKey(id));
    }

    /// <summary>
    /// Patch item in container.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="operations"></param>
    /// <returns></returns>
    public async Task PatchItemAsync(T item, List<PatchOperation> operations)
    {
        var key = ResolvePartitionKey(item.Id);
        await _container.PatchItemAsync<T>(item.Id, key, operations);
    }

    /// <summary>
    /// Patches Item or Adds Item if it doesn't exist.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="operations"></param>
    /// <returns></returns>
    public async Task PatchOrCreateSingleItemAsync(T item, List<PatchOperation> operations)
    {
        try
        {
            await PatchItemAsync(item, operations);
        }
        catch (CosmosException ce)
        {
            if (ce.StatusCode == System.Net.HttpStatusCode.NotFound) await AddItemAsync(item);
        }
    }

    /// <summary>
    /// Creates container in the database.
    /// </summary>
    public async Task GenerateContainer()
    {
        var containerProperties = GenerateContainerProperties();

        _ = _database.CreateContainerIfNotExistsAsync(containerProperties);
        //var t = _database.CreateContainerIfNotExistsAsync(containerProperties);
    }

    /// <summary>
    /// Gets container properties from repository.
    /// Creates container in the database.
    /// </summary>
    /// <returns>Container response.</returns>
    public async Task<ContainerResponse> GenerateContainerWithReturn()
    {
        var containerProperties = GenerateContainerProperties();
        return await _database.CreateContainerIfNotExistsAsync(containerProperties);
    }

    /// <summary>
    /// Used to see if there is an entry in the container by ids.
    /// </summary>
    /// <param name="idsToFind"></param>
    /// <returns>Container Ids</returns>
    public async Task<IEnumerable<BaseEntity>> GetIdsByIds(List<string> idsToFind)
    {
        List<(string, PartitionKey)> itemsToFind = new();

        foreach (var id in idsToFind)
        {
            itemsToFind.Add((id, ResolvePartitionKey(id)));
        }

        IEnumerable<BaseEntity> returnIds = await GetItemsByIdAndPartitionKeyAsync(itemsToFind);

        return returnIds;
    }

    /// <summary>
    /// Get container settings info from the settings json file.
    /// </summary>
    /// <returns></returns>
    public IContainerInfoEntity GetContainerSettingsInfo()
    {
        return _containerSettingsInfo;
    }

    /// <summary>
    /// Get database settings info from the settings json file.
    /// </summary>
    /// <returns></returns>
    public IDatabaseInfoEntity GetDatabaseSettingsInfo()
    {
        return _databaseInfo;
    }
}