

namespace ToDoList.Persistence.Repositories
{

    public interface IRepositoryAsync<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task DeleteByIdAsync(T item);
    }
}
