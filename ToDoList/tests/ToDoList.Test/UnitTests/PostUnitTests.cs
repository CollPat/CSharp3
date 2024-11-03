namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class PostUnitTests
{
    [Fact]
    public void Post_UnhandledRequest_ReturnsNewItem()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var item = request.ToDomain();
        var result = controller.Create(request);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var value = Assert.IsType<ToDoItem>(createdResult.Value);
        // Assert
        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);

    }
}
