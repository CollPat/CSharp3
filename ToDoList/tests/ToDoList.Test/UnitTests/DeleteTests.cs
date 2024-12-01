namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class DeleteTests
{
    [Fact]
    public void Delete_ValidItemId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
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
        var result = controller.DeleteByIdAsync(toDoItem.ToDoItemId);


        // Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).GetByIdAsync(toDoItem.ToDoItemId);
        repositoryMock.Received(1).DeleteAsync(toDoItem);

    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var invalidId = -1;
        repositoryMock.GetByIdAsync(Arg.Is(invalidId)).Returns((ToDoItem)null);

        // Act
        var result = controller.DeleteByIdAsync(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).GetByIdAsync(invalidId);
        repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Delete_UnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItemId = 1;
        repositoryMock.When(repo => repo.GetByIdAsync(toDoItemId)).Throw(_ => throw new Exception("Unhandled exception"));

        // Act
        var result = controller.DeleteByIdAsync(toDoItemId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        repositoryMock.Received(1).GetByIdAsync(toDoItemId);
        repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }

}
