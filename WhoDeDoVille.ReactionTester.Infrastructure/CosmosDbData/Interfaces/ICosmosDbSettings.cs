namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Interfaces;

public interface ICosmosDbSettings
{
    /// <summary>
    /// Set to true to by default check if database has been initiated.
    /// </summary>
    public string? RunInitiationCheck { get; set; }

    /// <summary>
    /// Connection String Value
    /// </summary>
    public string? CosmosReactiontesterConnectionString { get; set; }

    /// <summary>
    ///     Database name
    /// </summary>
    public IDatabaseInfoEntity Database { get; }

    ///// <summary>
    /////     List of containers in the database
    ///// </summary>
    //public Dictionary<string, ContainerInfoEntity> Containers { get; }
}
