namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData;

public class CosmosDbContainerFactory : ICosmosDbContainerFactory
{
    /// <summary>
    /// Azure Cosmos DB Client
    /// </summary>
    private readonly CosmosClient _cosmosClient;
    private IDatabaseInfoEntity _databaseInfo;
    private readonly bool _runInitiationCheck;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="cosmosClient"></param>
    /// <param name="databaseInfo"></param>
    /// <param name="runInitiationCheck"></param>
    public CosmosDbContainerFactory(CosmosClient cosmosClient, IDatabaseInfoEntity databaseInfo, string runInitiationCheck)
    {
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        _databaseInfo = databaseInfo ?? throw new ArgumentNullException(nameof(databaseInfo));
        _runInitiationCheck = Convert.ToBoolean(runInitiationCheck);
    }

    /// <summary>
    /// Checks if database has been marked as initialized.
    /// If it has not it will attempt to create the database.
    /// Marks database as initialized
    /// </summary>
    /// <returns>Database</returns>
    public ICosmosDbDatabase GetDatabase()
    {
        if (_runInitiationCheck == true && _databaseInfo.Initialized == false)
        {
            _ = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseInfo.DatabaseName);
            _databaseInfo.Initialized = true;
        }
        return new CosmosDbDatabase(_cosmosClient, _databaseInfo.DatabaseName);
    }

    /// <summary>
    /// Get Information on Database.
    /// </summary>
    /// <returns></returns>
    public IDatabaseInfoEntity GetDatabaseInfo()
    {
        return _databaseInfo;
    }

    /// <summary>
    /// Checks if container name is in the settings.json file.
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns>Container</returns>
    public ICosmosDbContainer GetContainer(string containerName)
    {
        return new CosmosDbContainer(_cosmosClient, _databaseInfo.DatabaseName, containerName);
    }
}