namespace ToDoList.Test;

using NSubstitute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class PutTests
{
    [Fact]
    public void Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
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
        var result = controller.UpdateByIdAsync(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        repositoryMock.Received(1).GetByIdAsync(toDoItem.ToDoItemId);

    }

    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
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
        var result = controller.UpdateByIdAsync(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        repositoryMock.Received(1).GetByIdAsync(invalidId);
        repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Name",
            Description = "Description",
            IsCompleted = false
        };

        repositoryMock.GetByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);
        repositoryMock.When(repo => repo.UpdateAsync(Arg.Any<ToDoItem>())).Throw(new Exception("Unhandled exception"));

        var request = new ToDoItemUpdateRequestDto(
            Name: "Another Name",
            Description: "Different description",
            IsCompleted: true,
            Category: "Work"

        );

        // Act
        var result = controller.UpdateByIdAsync(toDoItem.ToDoItemId, request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);
    }
}
