using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test;

public class CreateTest
{
    [Fact]
    public void Create_Item_ReturnsCreatedItem()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemsCreateRequestDto(
            Name: "New Task",
            Description: "Test task",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var createdAtActionResult = result as CreatedAtActionResult;

        // Assert
        Assert.NotNull(createdAtActionResult);
        Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        var returnValue = createdAtActionResult.Value as ToDoItem;
        Assert.NotNull(returnValue);
        Assert.Equal(1, returnValue.ToDoItemId);
    }


    [Fact]
    public void Create_Item_ReturnsInternalServerError_OnFailure()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemsCreateRequestDto(
            Name: "ThrowException",  // This will trigger an exception in the controller
            Description: "This task triggers an exception.",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
    }
}

