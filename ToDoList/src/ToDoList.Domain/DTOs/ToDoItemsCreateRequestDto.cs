
namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemsCreateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain()
    {
        return new ToDoItem
        {
            Name = Name,
            Description = Description,
            IsCompleted = IsCompleted
        };
    }
}
