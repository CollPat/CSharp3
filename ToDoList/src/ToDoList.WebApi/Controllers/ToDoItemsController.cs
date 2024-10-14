namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemsCreateRequestDto request)
    {

        var item = request.ToDomain();
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
        }

        // respond with the created item and its location
        return CreatedAtAction(nameof(ReadById), new { toDoItemId = item.ToDoItemId }, item); // 201 Created
    }

    [HttpGet]
    public IActionResult Read()
    {
        try
        {

            if (items == null)
            {
                return NotFound(); // 404 Not Found
            }

            var response = items.Select(ToDoItemGetResponseDto.FromDomain).ToList();
            return Ok(response); // 200 OK
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        try
        {
            var item = items.Find(x => x.ToDoItemId == toDoItemId);

            return item == null ? NotFound() : Ok(ToDoItemGetResponseDto.FromDomain(item));

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

    try
    {
        var index = items.FindIndex(x => x.ToDoItemId == toDoItemId);
        if (index == -1)
        {
            return NotFound(); // 404 Not Found
        }

        var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;
        items[index] = updatedItem;

        return NoContent(); // 204 No Content
    }
    catch (Exception ex)
    {
        return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
    }
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {

    try
    {
        var item = items.Find(x => x.ToDoItemId == toDoItemId);
        if (item == null)
        {
            return NotFound(); // 404 Not Found

        }

        items.Remove(item);
        return NoContent(); // 204 No Content
    }
    catch (Exception ex)
    {
        return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
    }
    }
}
