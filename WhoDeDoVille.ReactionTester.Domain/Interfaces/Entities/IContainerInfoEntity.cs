namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Entities;

public interface IContainerInfoEntity
{
    /// <summary>
    ///     Container Name
    /// </summary>
    public string? ContainerName { get; set; }

    /// <summary>
    ///     Container partition Key
    /// </summary>
    public string? PartitionKeyPath { get; set; }

    /// <summary>
    ///     Container is initialized
    /// </summary>
    public bool? IsInitialized { get; set; }
}
