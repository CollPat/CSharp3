namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;


public class GetTest
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //Arrange
        var controller = new ToDoItemsController();
        var ToDoItem = new ToDoItem();
        ToDoItemsController.items.Add(toDoItem);

        //Act
        var result = controller.Read();
        var value = result.value;
        result.resultResult = result.Result;
        //Assert
        Assert.True(resultResult is OkObjectResult);
        Assert.IsType<OkObjectResult>(resultResult);
    }
}
