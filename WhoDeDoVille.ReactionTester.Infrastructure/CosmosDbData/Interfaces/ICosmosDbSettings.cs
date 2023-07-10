namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Interfaces
{
    public interface ICosmosDbSettings
    {
        /// <summary>
        ///     CosmosDb Account - The Azure Cosmos DB endpoint
        /// </summary>
        public string? COSMOS_ENDPOINT { get; set; }

        /// <summary>
        ///     Key - The primary key for the Azure DocumentDB account.
        /// </summary>
        public string? COSMOS_KEY { get; set; }

        /// <summary>
        ///     Database name
        /// </summary>
        public DatabaseInfoEntity Database { get; set; }

        /// <summary>
        ///     List of containers in the database
        /// </summary>
        public Dictionary<string, ContainerInfoEntity>? Containers { get; set; }
    }
}
