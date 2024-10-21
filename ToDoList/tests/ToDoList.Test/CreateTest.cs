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
        //POZOR! tento test neni nezavisly! items v kontroleru je staticka promena, co to znamena? Vsechny objekty typu ToDoItemsController
        //sdili items, neni to tak ze ma kazdy svuj vlastni

        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = []; // tak, musel jsem jsem jeste upravit items aby nebyly readonly a je to :) ted uz pokazde tento test projde

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
        Assert.Equal(1, returnValue.ToDoItemId); //nemusi byt pravda, co kdyz nejaky predchazejici test dal neco do items?

        //skvele :) jeste bychom si teoreticky mohli zkontrolovat ze name, Description a IsCompleted je takove jake jsme zadali, protoze co kdyz
        //automaticky nastavuje IsCompleted na false? Tohle bychom mohli testovat a nejlepe parametrizovatelnym testem
    }


    [Fact]
    public void Create_Item_ReturnsInternalServerError_OnFailure()
    {
        //failuje, ale to je vec kontroleru

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

