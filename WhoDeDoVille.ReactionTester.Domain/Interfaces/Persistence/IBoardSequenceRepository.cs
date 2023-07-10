namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Persistence;

public interface IBoardSequenceRepository : IRepository<BoardSequenceEntity>
{
    string GenerateId(BoardSequenceEntity entity);

    PartitionKey ResolvePartitionKey(string entityId);

    ContainerProperties GenerateContainerProperties();
}
