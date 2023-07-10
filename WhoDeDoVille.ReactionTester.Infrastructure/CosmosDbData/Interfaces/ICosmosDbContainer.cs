namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Interfaces;

public interface ICosmosDbContainer
{
    /// <summary>
    ///     Instance of Azure Cosmos DB Container class
    /// </summary>
    Container _container { get; }
}