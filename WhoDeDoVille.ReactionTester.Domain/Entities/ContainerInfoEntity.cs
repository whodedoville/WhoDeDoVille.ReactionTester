namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class ContainerInfoEntity
{
    /// <summary>
    ///     Container Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Container partition Key
    /// </summary>
    public string? PartitionKey { get; set; }

    /// <summary>
    ///     Container is initialized
    /// </summary>
    public bool? Initialized { get; set; }
}
