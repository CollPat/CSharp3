namespace ToDoList.Test;

using NSubstitute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using System.Threading.Tasks;

public class PutTests
{
    [Fact]
    public async void Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Name",
            Description = "Description",
            IsCompleted = false
        };

        repositoryMock.GetByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Another name",
            Description: "Different description",
            IsCompleted: true,
            Category: "Work"
        );

        // Act
        var result = await controller.UpdateByIdAsync(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await repositoryMock.Received(1).GetByIdAsync(toDoItem.ToDoItemId);

    }

    [Fact]
    public async Task Put_InvalidId_ReturnsNotFoundAsync()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Another name",
            Description: "Different description",
            IsCompleted: true,
            Category: "Work"
        );

        var invalidId = -1;
        repositoryMock.GetByIdAsync(invalidId).Returns((ToDoItem)null);

        // Act
        var result = await controller.UpdateByIdAsync(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        await repositoryMock.Received(1).GetByIdAsync(invalidId);
        await repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMockAsync = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMockAsync);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Name",
            Description = "Description",
            IsCompleted = false
        };

        repositoryMockAsync.GetByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);
        repositoryMockAsync.When(repo => repo.UpdateAsync(Arg.Any<ToDoItem>())).Throw(new Exception("Unhandled exception"));

        var request = new ToDoItemUpdateRequestDto(
            Name: "Another Name",
            Description: "Different description",
            IsCompleted: true,
            Category: "Work"

        );

        // Act
        var result = await controller.UpdateByIdAsync(toDoItem.ToDoItemId, request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);

        await repositoryMockAsync.Received(1).GetByIdAsync(toDoItem.ToDoItemId);
        await repositoryMockAsync.Received(1).UpdateAsync(Arg.Any<ToDoItem>());
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(999, false)]
    public void Put_UpdateItem_ReturnsExpectedResult(int toDoItemId, bool exists)
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var existingItem = new ToDoItem
        {
            ToDoItemId = toDoItemId,
            Name = "Old Name",
            Description = "Old Description",
            IsCompleted = false,
            Category = "Work"
        };

        repositoryMock.GetByIdAsync(toDoItemId).Returns(exists ? existingItem : null);

        var controller = new ToDoItemsController(repositoryMock);


        var updatedItem = new ToDoItemUpdateRequestDto(
            Name: "Another Name",
            Description: "Different description",
            IsCompleted: true,
            Category: "Work"

        );

        // Act
        var result = controller.UpdateByIdAsync(toDoItemId, updatedItem);

        // Assert
        if (exists)
        {
            var noContentResult = Assert.IsType<NoContentResult>(result);
            repositoryMock.Received(1).UpdateAsync(Arg.Is<ToDoItem>(item =>
                item.ToDoItemId == toDoItemId &&
                item.Name == updatedItem.Name &&
                item.Description == updatedItem.Description &&
                item.IsCompleted == updatedItem.IsCompleted &&
                item.Category == updatedItem.Category
            ));
        }
        else
        {
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
