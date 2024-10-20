namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;


public class GetTest
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem { ToDoItemId = 1 };
        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.Read();
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.NotNull(okResult);
        Assert.IsType<OkObjectResult>(okResult);
        var returnValue = okResult.Value as IEnumerable<ToDoItemGetResponseDto>;
        Assert.NotNull(returnValue);
        Assert.Single(returnValue);
    }

    [Fact]
    public void Get_ItemById_ReturnsItem_WhenIdIsValid()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem { ToDoItemId = 1 };
        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.ReadById(1);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.NotNull(okResult);
        Assert.IsType<OkObjectResult>(okResult);
        var returnValue = okResult.Value as ToDoItemGetResponseDto;
        Assert.NotNull(returnValue);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public void Get_ItemById_ReturnsNotFound_WhenIdIsInvalid()
    {
        // Arrange
        var controller = new ToDoItemsController();

        // Act
        var result = controller.ReadById(99);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.NotNull(notFoundResult);
        Assert.IsType<NotFoundResult>(notFoundResult);
    }
}
