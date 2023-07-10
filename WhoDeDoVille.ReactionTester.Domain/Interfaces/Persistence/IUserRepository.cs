using User = WhoDeDoVille.ReactionTester.Domain.Entities.User;

namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetItemsAsyncByID(string Id);
    Task<IEnumerable<User>> GetItemsAsyncByEmail(string email);
}