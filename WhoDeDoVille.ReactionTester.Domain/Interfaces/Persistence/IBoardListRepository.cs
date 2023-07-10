namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Persistence;

public interface IBoardListRepository : IRepository<BoardListEntity>
{
    string GenerateId(BoardListEntity entity);

    PartitionKey ResolvePartitionKey(string entityId);

    ContainerProperties GenerateContainerProperties();
}
