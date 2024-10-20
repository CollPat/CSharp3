using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test;

public class UpdateTest
{
    [Fact]
    public void Update_ItemById_UpdatesItem_WhenIdIsValid()
    {
        // Arrange

        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem { ToDoItemId = 1, Name = "Old Task" };
        ToDoItemsController.items.Add(toDoItem);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Updated Task",
            Description: "Not found",
            IsCompleted: true
        );
        // Act
        var result = controller.UpdateById(1, request);
        var noContentResult = result as NoContentResult;

        // Assert
        Assert.NotNull(noContentResult);
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.Equal("Updated Task", ToDoItemsController.items.First().Name);
    }

    [Fact]
    public void Update_ItemById_ReturnsNotFound_WhenIdIsInvalid()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemUpdateRequestDto(
            Name: "Updated Task",
            Description: "Not found",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(99, request);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.NotNull(notFoundResult);
        Assert.IsType<NotFoundResult>(notFoundResult);
    }
}
