

namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Persistence;

public interface IBoardRepository : IRepository<BoardEntity>
{
    string GenerateId(BoardEntity entity);

    PartitionKey ResolvePartitionKey(string entityId);

    ContainerProperties GenerateContainerProperties();
}
