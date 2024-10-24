namespace ToDoList.Persistence;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsContext : DbContext
{
    private readonly string connectionString;
    public ToDoItemsContext(string connectionString= "Data source= ../../data/localdb.db")
    {
        this.connectionString = connectionString;
    }

    public DbSet<ToDoItem> ToDoItems {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }
}
