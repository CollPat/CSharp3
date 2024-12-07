namespace ToDoList.Test;

using System.Threading.Tasks;
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
    public async void Get_SomeItemsAvailable_ReturnsAllItems()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Test Name",
            Description = "Test Decription",
            IsCompleted = false,
            Category = "Work"
        };


        var toDoItems = new List<ToDoItem> { toDoItem };

        repositoryMock.GetAllAsync().Returns(toDoItems);

        // Act
        var result = await controller.ReadAsync();

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

        await repositoryMock.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task Get_NoItems_ReturnsNotFoundAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.GetAllAsync().Returns(new List<ToDoItem>());

        // Act
        var result = await controller.ReadAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

        await repositoryMock.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task Get_ReadUnhandledException_ReturnsInternalServerErrorAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        repositoryMock.GetAllAsync().Throws(x => { throw new Exception("Unhandled exception"); });

        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = await controller.ReadAsync();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);
    }

}
