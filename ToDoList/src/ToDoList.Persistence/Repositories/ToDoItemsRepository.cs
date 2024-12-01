namespace ToDoList.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;

    }
    public async Task CreateAsync(ToDoItem item)
    {
        await context.ToDoItems.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task<ToDoItem> GetByIdAsync(int id)
    {
        return await context.ToDoItems.FindAsync(id);
    }

    public async Task<IEnumerable<ToDoItem>> GetAllAsync()
    {
        return await context.ToDoItems.ToListAsync();
    }

    public async Task UpdateAsync(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ToDoItem item)
    {
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}

