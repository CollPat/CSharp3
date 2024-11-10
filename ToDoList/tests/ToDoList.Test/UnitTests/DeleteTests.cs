namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
            IsCompleted = false
        };
        repositoryMock.GetById(Arg.Any<int>()).Returns(toDoItem);

        // Act
        var result = controller.DeleteById(toDoItem.ToDoItemId);


        // Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).GetById(toDoItem.ToDoItemId);
        repositoryMock.Received(1).Delete(toDoItem);

    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var invalidId = -1;
        repositoryMock.GetById(Arg.Is(invalidId)).Returns((ToDoItem)null);

        // Act
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).GetById(invalidId);
        repositoryMock.DidNotReceive().Delete(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Delete_UnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItemId = 1;
        repositoryMock.GetById(Arg.Any<int>()).Throws(new Exception("Unhandled exception"));

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        repositoryMock.Received(1).GetById(toDoItemId);
        repositoryMock.DidNotReceive().Delete(Arg.Any<ToDoItem>());
    }

}
