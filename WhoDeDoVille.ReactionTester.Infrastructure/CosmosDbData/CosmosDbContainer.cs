namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData;

public class CosmosDbContainer : ICosmosDbContainer
{
    public Container _container { get; }

    public CosmosDbContainer(CosmosClient cosmosClient,
                             string databaseName,
                             string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }
}