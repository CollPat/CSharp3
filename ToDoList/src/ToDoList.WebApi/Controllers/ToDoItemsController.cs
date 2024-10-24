namespace ToDoList.WebApi.Controllers;

using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase


{
    private readonly ToDoItemsContext context;
    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult Create(ToDoItemsCreateRequestDto request)
    {

        var item = request.ToDomain();
        try
        {
            if (context.ToDoItems.Any(o => o.ToDoItemId == item.ToDoItemId))
            {
                throw new Exception("Duplicate item ID");
            }

            context.ToDoItems.Add(item);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }


        return CreatedAtAction(nameof(ReadById), new { toDoItemId = item.ToDoItemId }, item);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        try
        {

            var response = context.ToDoItems.Select(ToDoItemGetResponseDto.FromDomain).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        try
        {
            var item = context.ToDoItems.Find(toDoItemId);

            return item == null ? NotFound() : Ok(context.ToDoItems.Select(ToDoItemGetResponseDto.FromDomain));

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        try
        {
            var item = context.ToDoItems.Find(toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            var updatedItem = request.ToDomain();
            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;

            context.ToDoItems.Update(item);
            context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {

        try
        {
            var item = context.ToDoItems.Find(toDoItemId);
            if (item == null)
            {
                return NotFound();

            }

            context.ToDoItems.Remove(item);
            context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
