
namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemUpdateRequestDto(string Name, string Description, bool IsCompleted, string? Category)
{

    public ToDoItem ToDomain() => new ToDoItem
    {
        Name = Name,
        Description = Description,
        IsCompleted = IsCompleted,
        Category = Category
    };
}
