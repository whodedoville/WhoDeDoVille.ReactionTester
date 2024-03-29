﻿namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class DatabaseInfoEntity : IDatabaseInfoEntity
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
