namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Interfaces
{
    public interface ICosmosDbDatabase
    {
        /// <summary>
        ///     Instance of Azure Cosmos DB Database class
        /// </summary>
        Database _database { get; }
    }
}
