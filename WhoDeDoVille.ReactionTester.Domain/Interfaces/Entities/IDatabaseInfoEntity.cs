namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Entities;

public interface IDatabaseInfoEntity
{
    /// <summary>
    ///     Database name
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    ///     Database initialized
    /// </summary>
    public bool? Initialized { get; set; }
}
