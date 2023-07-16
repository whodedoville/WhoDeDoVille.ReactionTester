namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Interfaces;

public interface ICosmosDbContainerFactory
{
    /// <summary>
    ///     Returns a CosmosDbContainer wrapper
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    ICosmosDbContainer GetContainer(string containerName);

    /// <summary>
    ///     Returns a CosmosDbDatabase wrapper
    /// </summary>
    /// <returns></returns>
    ICosmosDbDatabase GetDatabase();

    /// <summary>
    /// Get Information on Database.
    /// </summary>
    /// <returns></returns>
    IDatabaseInfoEntity GetDatabaseInfo();
}