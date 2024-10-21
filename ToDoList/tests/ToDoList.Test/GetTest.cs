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
        //opet test neni nezavisly - me napriklad konci failem kdyz spustim vsechny testy

        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = []; //a je to :)
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


    //ReadById a Read jsou dve samostatne metody, lepsi by teda bylo mit 2 samostatne soubory s test, jeden pro Read metodu a jeden pro ReadById
    [Fact]
    public void Get_ItemById_ReturnsItem_WhenIdIsValid()
    {
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = []; //opet :)
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

        //mohli bychom jeste testovat ze to vraci nezmenene Name,Description,IsCompleted
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
