namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Repository
{
    //TODO: Add testing
    public class BoardRepository : CosmosDbRepository<BoardEntity>, IBoardRepository
    {
        /// <summary>
        /// Container name used when creating container
        /// </summary>
        public override string ContainerName { get; } = "Board";

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
        public override string GenerateId(BoardEntity entity) => "";

        /// <summary>
        /// Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId)
        {
            var splitId = entityId.Split(':');
            return new($"{splitId[0]}:{splitId[1]}:{splitId[2].Substring(0, 6)}");
        }

        public BoardRepository(ICosmosDbContainerFactory factory, ICosmosDbSettings cosmosDbSettings) :
            base(factory, cosmosDbSettings.Containers["Board_Container"]!, cosmosDbSettings.Database!)
        { }

        /// <summary>
        /// Container properties
        /// </summary>
        /// <returns>Board Container Properties</returns>
        public override ContainerProperties GenerateContainerProperties()
        {
            ContainerProperties containerProperties = new ContainerProperties(ContainerName, PartitionKeyPath);
            containerProperties.IndexingPolicy = GetIndexingPolicy();
            containerProperties.UniqueKeyPolicy = GetUniqueKeyPolicy();
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
            indexingPolicy.IncludedPaths.Add(new IncludedPath() { Path = $"{PartitionKeyPath}/?" });

            return indexingPolicy;
        }

        /// <summary>
        /// Unique key policy used with container properties
        /// </summary>
        /// <returns>Unique key policy options</returns>
        private UniqueKeyPolicy GetUniqueKeyPolicy()
        {
            var uniqueKeyPolicy = new UniqueKeyPolicy();

            uniqueKeyPolicy.UniqueKeys.Add(new UniqueKey() { Paths = { "/difficulty", PartitionKeyPath } });

            return uniqueKeyPolicy;
        }
    }
}
