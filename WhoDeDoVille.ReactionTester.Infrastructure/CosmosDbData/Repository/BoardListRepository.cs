﻿using Microsoft.Extensions.Logging;

namespace WhoDeDoVille.ReactionTester.Infrastructure.CosmosDbData.Repository;

//TODO: Add testing
public class BoardListRepository : CosmosDbRepository<BoardListEntity>, IBoardListRepository
{
    /// <summary>
    /// Cosmos DB container Settings
    /// </summary>
    private static ContainerInfoEntity ContainerSettingsInfo { get; set; } = new()
    {
        ContainerName = "BoardList",
        PartitionKeyPath = "/sequencenumber",
        IsInitialized = false,
    };

    ///// <summary>
    ///// Container name used when creating container
    ///// </summary>
    //public override string ContainerName { get; } = "BoardList";

    ///// <summary>
    ///// Container name used when creating container
    ///// </summary>
    //public override string PartitionKeyPath { get; } = "/sequence";

    /// <summary>
    /// Generate Id.
    /// e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public override string GenerateId(BoardListEntity entity) => $"{entity.Difficulty}:{entity.SequenceNumber}";

    /// <summary>
    /// Returns the value of the partition key
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public override PartitionKey ResolvePartitionKey(string entityId) => new(entityId.Split(':')[1]);


    public BoardListRepository(ICosmosDbContainerFactory factory, ILogger<BoardListRepository> logger) :
        base(factory, logger, ContainerSettingsInfo)
    { }

    /// <summary>
    /// Container properties
    /// </summary>
    /// <returns>Board List Container Properties</returns>
    public override ContainerProperties GenerateContainerProperties()
    {
        ContainerProperties containerProperties = new ContainerProperties(ContainerSettingsInfo.ContainerName, ContainerSettingsInfo.PartitionKeyPath);
        containerProperties.IndexingPolicy = GetIndexingPolicy();
        return containerProperties;
    }

    /// <summary>
    /// Indexing policy used with container properties
    /// </summary>
    /// <returns>Indexing policy options</returns>
    private IndexingPolicy GetIndexingPolicy()
    {
        var indexingPolicy = new IndexingPolicy();

        indexingPolicy.IndexingMode = IndexingMode.Consistent;
        indexingPolicy.Automatic = true;
        indexingPolicy.ExcludedPaths.Add(new ExcludedPath() { Path = "/*" });

        return indexingPolicy;
    }
}
