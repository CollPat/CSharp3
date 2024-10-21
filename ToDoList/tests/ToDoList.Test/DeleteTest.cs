using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test;

public class DeleteTest
{
    [Fact]
    public void Delete_ItemById_RemovesItem_WhenIdIsValid()
    {
        //test opet neni nezavisly

        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem { ToDoItemId = 1 };

        ToDoItemsController.items = [];
        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.DeleteById(1);
        var noContentResult = result as NoContentResult;

        // Assert
        Assert.NotNull(noContentResult);
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.Empty(ToDoItemsController.items);

        //odchytil by nam tento test pripad ze DeleteById maze cely obsah items? ;)
    }

    [Fact]
    public void Delete_ItemById_ReturnsNotFound_WhenIdIsInvalid()
    {
        // Arrange
        var controller = new ToDoItemsController();

        // Act
        var result = controller.DeleteById(99);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.NotNull(notFoundResult);
        Assert.IsType<NotFoundResult>(notFoundResult);
    }
}

