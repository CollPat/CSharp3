namespace ToDoList.Test;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class PostUnitTests
{

    [Fact]
    public void Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        repositoryMock.When(repo => repo.Create(Arg.Any<ToDoItem>())).Throw(_ => { throw new Exception("Unhandled exception"); });

        var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemsCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var value = Assert.IsType<ToDoItem>(createdResult.Value);
        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);

        repositoryMock.Received(1).Create(Arg.Any<ToDoItem>());

    }

    [Fact]
    public void Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        repositoryMock.When(repo => repo.Create(Arg.Any<ToDoItem>())).Throw(_ => { throw new Exception("Unhandled exception"); });

        var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemsCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);

        repositoryMock.Received(1).Create(Arg.Any<ToDoItem>());
    }
}

