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
    /// <param name="databaseName"></param>
    /// <returns></returns>
    ICosmosDbDatabase GetDatabase(string databaseName);

    /// <summary>
    ///     Ensure the database is created
    /// </summary>
    /// <returns></returns>
    Task EnsureDbSetupAsync();
}