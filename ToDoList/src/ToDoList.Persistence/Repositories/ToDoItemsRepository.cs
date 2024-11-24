namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;

    }
    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public ToDoItem GetById(int id)
    {
        return context.ToDoItems.Find(id);
    }

    public IEnumerable<ToDoItem> GetAll()
    {
        return context.ToDoItems.ToList();
    }

    public void Update(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var item = context.ToDoItems.Find(id);
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            context.SaveChanges();
        }
    }

    public void Delete(ToDoItem item)
    {
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            context.SaveChanges();
        }
    }
}

