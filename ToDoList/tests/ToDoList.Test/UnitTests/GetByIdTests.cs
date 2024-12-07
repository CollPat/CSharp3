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

public class GetByIdTests
{
    [Fact]
    public async Task GetById_ValidId_ReturnsItemAsync()
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
        var result = await controller.GetByIdAsync(toDoItem.ToDoItemId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);

        Assert.Equal(toDoItem.ToDoItemId, value.Id);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);
        Assert.Equal(toDoItem.Name, value.Name);
        Assert.Equal(toDoItem.Category, value.Category);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFoundAsync()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var invalidId = -1;

        // Act
        var result = await controller.GetByIdAsync(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Get_ByIdUnhandledException_ReturnsInternalServerErrorAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        repositoryMock.GetByIdAsync(Arg.Any<int>()).Throws(x => { throw new Exception("Unhandled exception"); });

        var controller = new ToDoItemsController(repositoryMock);
        var validId = 1;

        // Act
        var result = await controller.GetByIdAsync(validId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(999, false)]

    public void GetById_ReturnsExpectedResult(int toDoItemId, bool exists)
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        if (exists)
        {
            repositoryMock.GetByIdAsync(toDoItemId).Returns(new ToDoItem
            {
                ToDoItemId = toDoItemId,
                Name = "Test Name",
                Description = "Test Description",
                IsCompleted = false,
                Category = "Work"
            });
        }
        else
        {
            repositoryMock.GetByIdAsync(toDoItemId).Returns((ToDoItem)null);
        }

        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = controller.GetByIdAsync(toDoItemId);

        // Assert
        if (exists)
        {
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<ToDoItem>(okResult.Value);
            Assert.Equal(toDoItemId, item.ToDoItemId);
        }
        else
        {
            Assert.IsType<NotFoundResult>(result);
        }

        repositoryMock.Received(1).GetByIdAsync(toDoItemId);
    }
}
