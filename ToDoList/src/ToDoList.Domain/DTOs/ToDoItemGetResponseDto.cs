
namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemGetResponseDto(int Id, string Name, string Description, bool IsCompleted)
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new ToDoItemGetResponseDto(
            item.ToDoItemId,
            item.Name,
            item.Description,
            item.IsCompleted
        );
}
