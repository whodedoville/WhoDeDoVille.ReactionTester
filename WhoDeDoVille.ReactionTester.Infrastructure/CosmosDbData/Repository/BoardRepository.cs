namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Repository
{
    //TODO: Add testing
    public class BoardRepository : CosmosDbRepository<BoardEntity>, IBoardRepository
    {
        /// <summary>
        /// Cosmos DB container Settings
        /// </summary>
        private static ContainerInfoEntity ContainerSettingsInfo { get; set; } = new()
        {
            ContainerName = "Board",
            PartitionKeyPath = "/partitionkey",
            IsInitialized = false,
        };
        ///// <summary>
        ///// Container name used when creating container
        ///// </summary>
        //public override string ContainerName { get; } = "Board";

        ///// <summary>
        ///// Container name used when creating container
        ///// </summary>
        //public override string PartitionKeyPath { get; } = "/partitionkey";

        ///// <summary>
        ///// Container is initialized.
        ///// </summary>
        //public override bool IsInitialized {  get; } = false;

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

        public BoardRepository(ICosmosDbContainerFactory factory) :
            base(factory, ContainerSettingsInfo)
        { }

        /// <summary>
        /// Container properties
        /// </summary>
        /// <returns>Board Container Properties</returns>
        public override ContainerProperties GenerateContainerProperties()
        {
            ContainerProperties containerProperties = new ContainerProperties(ContainerSettingsInfo.ContainerName, ContainerSettingsInfo.PartitionKeyPath);
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
            indexingPolicy.IncludedPaths.Add(new IncludedPath() { Path = $"{ContainerSettingsInfo.PartitionKeyPath}/?" });

            return indexingPolicy;
        }

        /// <summary>
        /// Unique key policy used with container properties
        /// </summary>
        /// <returns>Unique key policy options</returns>
        private UniqueKeyPolicy GetUniqueKeyPolicy()
        {
            var uniqueKeyPolicy = new UniqueKeyPolicy();

            uniqueKeyPolicy.UniqueKeys.Add(new UniqueKey() { Paths = { "/difficulty", ContainerSettingsInfo.PartitionKeyPath } });

            return uniqueKeyPolicy;
        }
    }
}
