namespace ToDoLIst_DAL.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<int> CountAsync();
    }
}
