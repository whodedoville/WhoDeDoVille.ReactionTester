namespace WhoDeDoVille.ReactionTester.Infrastructure.AppSettings;

public class CosmosDbSettings : ICosmosDbSettings
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
    ///     Database info
    /// </summary>
    public IDatabaseInfoEntity Database { get; } = new DatabaseInfoEntity()
    {
        DatabaseName = "ReactionTester",
        Initialized = false
    };

    ///// <summary>
    /////     List of containers in the database
    ///// </summary>
    //public Dictionary<string, ContainerInfoEntity> Containers { get; } = new Dictionary<string, ContainerInfoEntity>()
    //{
    //    {"User_Container", new ContainerInfoEntity()
    //    {
    //        ContainerName = "User",
    //        PartitionKeyPath = "/UserId",
    //        IsInitialized = false
    //    } },
    //    {"BoardSequence_Container", new ContainerInfoEntity()
    //    {
    //        ContainerName = "BoardSequence",
    //        PartitionKeyPath = "/partitionkey",
    //        IsInitialized = false
    //    } },
    //    {"Board_Container", new ContainerInfoEntity()
    //    {
    //        ContainerName = "Board",
    //        PartitionKeyPath = "/partitionkey",
    //        IsInitialized = false
    //    } },
    //    {"BoardList_Container", new ContainerInfoEntity()
    //    {
    //        ContainerName = "BoardList",
    //        PartitionKeyPath = "/sequence",
    //        IsInitialized = false
    //    } }
    //};
}
