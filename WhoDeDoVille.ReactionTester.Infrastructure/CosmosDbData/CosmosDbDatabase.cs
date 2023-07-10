namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData;

public class CosmosDbDatabase : ICosmosDbDatabase
{
    public Database _database { get; }

    public CosmosDbDatabase(CosmosClient cosmosClient, string databaseName)
    {
        _database = cosmosClient.GetDatabase(databaseName);
    }
}
