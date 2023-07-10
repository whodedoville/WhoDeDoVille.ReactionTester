namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData;

public class CosmosDbContainerFactory : ICosmosDbContainerFactory
{
    /// <summary>
    /// Azure Cosmos DB Client
    /// </summary>
    private readonly CosmosClient _cosmosClient;
    private readonly DatabaseInfoEntity _databaseInfo;
    private readonly Dictionary<string, ContainerInfoEntity> _containers;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="cosmosClient"></param>
    /// <param name="databaseInfo"></param>
    /// <param name="containers"></param>
    public CosmosDbContainerFactory(CosmosClient cosmosClient,
                               DatabaseInfoEntity databaseInfo,
                               Dictionary<string, ContainerInfoEntity> containers)
    {
        _databaseInfo = databaseInfo ?? throw new ArgumentNullException(nameof(databaseInfo));
        _containers = containers ?? throw new ArgumentNullException(nameof(containers));
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
    }

    /// <summary>
    /// Checks if container name is in the settings.json file.
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns>Container</returns>
    public ICosmosDbContainer GetContainer(string containerName)
    {
        if (_containers.Where(x => x.Value.Name == containerName) == null)
        {
            throw new ArgumentException($"Unable to find container: {containerName}");
        }

        return new CosmosDbContainer(_cosmosClient, _databaseInfo.DatabaseName, containerName);
    }

    /// <summary>
    /// Checks if database has been marked as initialized.
    /// If it has not it will attempt to create the database.
    /// Marks database as initialized
    /// </summary>
    /// <param name="databaseName"></param>
    /// <returns>Database</returns>
    public ICosmosDbDatabase GetDatabase(string databaseName)
    {
        if (_databaseInfo.Initialized == false)
        {
            _ = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseInfo.DatabaseName);
            _databaseInfo.Initialized = true;
        }
        return new CosmosDbDatabase(_cosmosClient, _databaseInfo.DatabaseName);
    }

    public async Task EnsureDbSetupAsync()
    {
        DatabaseResponse database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseInfo.DatabaseName);

        foreach (KeyValuePair<string, ContainerInfoEntity> container in _containers)
        {
            var containerInfo = container.Value;

            await database.Database.CreateContainerIfNotExistsAsync(containerInfo.Name, $"{containerInfo.PartitionKey}");
        }
    }
}