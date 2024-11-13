namespace ToDoList.Test;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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

        repositoryMock.GetById(toDoItem.ToDoItemId).Returns(toDoItem);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Another name",
            Description: "Different description",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        //opet bychom mohli kontrolovat jake metody a kolikrat z kontroleru jsme volali pres mock
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
            IsCompleted: true
        );

        var invalidId = -1;
        repositoryMock.GetById(invalidId).Returns((ToDoItem)null);

        // Act
        var result = controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        //opet bychom mohli kontrolovat jake metody a kolikrat z kontroleru jsme volali pres mock
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

        repositoryMock.GetById(toDoItem.ToDoItemId).Returns(toDoItem);
        repositoryMock.When(repo => repo.Update(Arg.Any<ToDoItem>())).Do(_ => { throw new Exception("Unhandled exception"); });
        //repositoryMock.When(repo => repo.Update(Arg.Any<ToDoItem>())).Throws(new Exception("Unhandled exception"));
        //na vyhazovani vyjimek je lepsi Throws

        var request = new ToDoItemUpdateRequestDto(
            Name: "Another Name",
            Description: "Different description",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Unhandled exception", problemDetails.Detail);
    }
}
