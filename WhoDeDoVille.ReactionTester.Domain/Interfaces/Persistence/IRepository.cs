namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Persistence;

public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Get items given a string SQL query directly.
    ///     Likely in production, you may want to use alternatives like Parameterized Query or LINQ to avoid SQL Injection and avoid having to work with strings directly.
    ///     This is kept here for demonstration purpose.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetItemsAsync(string query);

    /// <summary>
    ///     Get one item by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetItemAsync(string id);

    /// <summary>
    ///     Add one item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task AddItemAsync(T item);

    /// <summary>
    ///     Add many items.
    ///     Uses Needs Bulk execution enabled.
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    Task AddItemsAsync(List<T> items);

    /// <summary>
    ///     Update one item by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    Task UpdateItemAsync(string id, T item);

    /// <summary>
    ///     Delete one item by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteItemAsync(string id);

    /// <summary>
    /// Patch Item.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="operations"></param>
    /// <returns></returns>
    Task PatchItemAsync(T item, List<PatchOperation> operations);

    /// <summary>
    ///     Patch or Create Item.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="operations"></param>
    /// <returns></returns>
    Task PatchOrCreateSingleItemAsync(T item, List<PatchOperation> operations);

    /// <summary>
    ///     Generate container for entity.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task GenerateContainer();

    /// <summary>
    /// Gets container properties from repository.
    /// Creates container in the database.
    /// </summary>
    /// <returns>Container response.</returns>
    Task<ContainerResponse> GenerateContainerWithReturn();

    /// <summary>
    /// Searches for Ids in database.
    /// </summary>
    /// <remarks>Used to check on board duplication mostly.</remarks>
    /// <param name="idsToFind">List of Ids to search.</param>
    /// <returns></returns>
    Task<IEnumerable<BaseEntity>> GetIdsByIds(List<string> idsToFind);

    /// <summary>
    /// Get container settings info from the settings json file.
    /// </summary>
    /// <returns></returns>
    IContainerInfoEntity GetContainerSettingsInfo();

    /// <summary>
    /// Get database settings info from the settings json file.
    /// </summary>
    /// <returns></returns>
    IDatabaseInfoEntity GetDatabaseSettingsInfo();
}