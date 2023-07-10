namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Repository;

public class BoardSequenceRepository : CosmosDbRepository<BoardSequenceEntity>, IBoardSequenceRepository
{
    /// <summary>
    /// Container name used when creating container
    /// </summary>
    public override string ContainerName { get; } = "BoardSequence";

    /// <summary>
    /// Container name used when creating container
    /// </summary>
    public override string PartitionKeyPath { get; } = "/partitionkey";

    /// <summary>
    /// Generate Id.
    /// e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public override string GenerateId(BoardSequenceEntity entity) => "";

    /// <summary>
    /// Returns the value of the partition key
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public override PartitionKey ResolvePartitionKey(string entityId)
    {
        var splitId = entityId.Split(':');
        return new($"{splitId[0]}");
    }

    public BoardSequenceRepository(ICosmosDbContainerFactory factory, ICosmosDbSettings cosmosDbSettings) :
            base(factory, cosmosDbSettings.Containers["BoardSequence_Container"]!, cosmosDbSettings.Database!)
    { }

    /// <summary>
    /// Container properties
    /// </summary>
    /// <returns>Board Container Properties</returns>
    public override ContainerProperties GenerateContainerProperties()
    {
        ContainerProperties containerProperties = new ContainerProperties(ContainerName, PartitionKeyPath);
        containerProperties.IndexingPolicy = GetIndexingPolicy();
        return containerProperties;
    }

    /// <summary>
    /// Indexing policy used with container properties
    /// </summary>
    /// <returns>Indexing policy options</returns>
    private IndexingPolicy GetIndexingPolicy()
    {
        var indexingPolicy = new IndexingPolicy();

        indexingPolicy.IndexingMode = IndexingMode.Consistent;
        indexingPolicy.Automatic = true;
        indexingPolicy.ExcludedPaths.Add(new ExcludedPath() { Path = "/*" });

        return indexingPolicy;
    }
}
