namespace ToDoList.Test;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class DeleteTests
{
    [Fact]
    public async Task Delete_ValidItemId_ReturnsNoContentAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false,
            Category = "Work"
        };

        repositoryMock.GetByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);

        // Act
        var result = await controller.DeleteByIdAsync(toDoItem.ToDoItemId);


        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).GetByIdAsync(toDoItem.ToDoItemId);
        await repositoryMock.Received(1).DeleteAsync(toDoItem);

    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFoundAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var invalidId = -1;
        repositoryMock.GetByIdAsync(Arg.Is(invalidId)).Returns((ToDoItem)null);

        // Act
        var result = await controller.DeleteByIdAsync(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        await repositoryMock.Received(1).GetByIdAsync(invalidId);
        await repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async Task Delete_UnhandledException_ReturnsInternalServerErrorAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItemId = 1;
        repositoryMock.When(repo => repo.GetByIdAsync(toDoItemId)).Throw(_ => throw new Exception("Unhandled exception"));

        // Act
        var result = await controller.DeleteByIdAsync(toDoItemId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        await repositoryMock.Received(1).GetByIdAsync(toDoItemId);
        await repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }
}
