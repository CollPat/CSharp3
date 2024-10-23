namespace ToDoList.Domain.Models;

using System.ComponentModel.DataAnnotations;

public class ToDoItem
{

    [Key]
    public int ToDoItemId { get; set; }
    [Length(1, 50)]
    public string Name { get; set; }
    [StringLength(250)]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }

}
