namespace ToDoList.Test;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetTests
{
    [Fact]
    public void Get_SomeItemsAvailable_ReturnsAllItems()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Test Name",
            Description = "Test Decription",
            IsCompleted = false,
            Category = "Work"
        };


        var toDoItems = new List<ToDoItem> { toDoItem }; //viz nasledujici comment. Pokud si nechas sve reseni, tak tento radek smazat - nemeli bychom nechavat zbytecny kod

        repositoryMock.GetAll().Returns(new List<ToDoItem> { toDoItem }); //muzeme tady dat repositoryMock.GetAll().Returns(toDoItems);

        // Act
        var result = controller.Read();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IEnumerable<ToDoItemGetResponseDto>>(okResult.Value);

        Assert.Single(value);

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);
        Assert.Equal(toDoItem.Category, firstItem.Category);

        repositoryMock.Received(1).GetAll();
    }

    [Fact]
    public void Get_NoItems_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.GetAll().Returns(new List<ToDoItem>());

        // Act
        var result = controller.Read();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

        repositoryMock.Received(1).GetAll();
    }

    [Fact]
    public void Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        repositoryMock.GetAll().Throws(x => { throw new Exception("Unhandled exception"); });

        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = controller.Read();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);
    }

}
