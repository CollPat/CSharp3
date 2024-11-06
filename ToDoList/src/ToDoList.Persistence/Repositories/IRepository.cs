

namespace ToDoList.Persistence.Repositories
{
    public interface IRepository<T> where T : class
    {
        public T? GetById(int id);
        public IEnumerable<T> GetAll();
        public void Create(T item);

        public void Update(T item);

        public void Delete (T item);
    }
}
