namespace ToDoList.Persistence;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsContext : DbContext
{
    private readonly string connectionstring;
    public ToDoItemsContext(string connectionstring= "Data source=../data/localdb.db")
    {
        this.connectionstring = connectionstring;
    }

    public DbSet<ToDoItem> ToDoItems {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionstring);
    }
}
