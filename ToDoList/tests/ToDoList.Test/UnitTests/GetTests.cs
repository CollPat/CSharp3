namespace ToDoList.Test;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems() //nazev je zajimavy :) radeji bych to oznacil Get_SomeItemsAvailable_ReturnsAllItems
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Test Name",
            Description = "Test Decription",
            IsCompleted = false
        };

        //zde bych predpripravil List<ToDoItem> ktery v sobe ma toDoItem

        repositoryMock.GetAll().Returns(new List<ToDoItem> { toDoItem });

        // Act
        var result = controller.Read();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IEnumerable<ToDoItemGetResponseDto>>(okResult.Value);

        //chybi mi kontrola zda se vratil spravny pocet ToDoItems (ocekavam ze v IEnumerable dostanu 1 v tvem pridape a ten bych taky mel dostat)

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);

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
        repositoryMock.GetAll().Returns(x => { throw new Exception("Unhandled exception"); });
        //na vyhazovani vyjimek je lepsi Throw

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
