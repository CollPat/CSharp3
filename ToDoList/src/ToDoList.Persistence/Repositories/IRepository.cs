

namespace ToDoList.Persistence.Repositories
{
    //chtelo by to nazev IRepositoryAsync - jednak je to v zadani, jednak je to slusnost napsat kdyz mame asynchronni operace + kdyz pak zmenis nazev, poprosim reflektovat v nazvu souboru
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
