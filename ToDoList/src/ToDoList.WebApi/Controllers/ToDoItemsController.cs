namespace ToDoList.WebApi.Controllers;

using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public static List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemsCreateRequestDto request)
    {

        var item = request.ToDomain();
        try
        {
            //vidim ze se pokousis o vyvolani InternalServerError, ale tato vyjimka vyskoci jen kdyz v items je ToDoItem co ma id = 0
            if (items.Any(o => o.ToDoItemId == item.ToDoItemId))
            {
                throw new Exception("Duplicate item ID");
            }

            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
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

            if (items == null)
            {
                return NotFound();
            }

            var response = items.Select(ToDoItemGetResponseDto.FromDomain).ToList();
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
            var item = items.Find(x => x.ToDoItemId == toDoItemId);

            return item == null ? NotFound() : Ok(ToDoItemGetResponseDto.FromDomain(item));

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
            var index = items.FindIndex(x => x.ToDoItemId == toDoItemId);
            if (index == -1)
            {
                return NotFound();
            }

            var updatedItem = request.ToDomain();
            updatedItem.ToDoItemId = toDoItemId;
            items[index] = updatedItem;

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
            var item = items.Find(x => x.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();

            }

            items.Remove(item);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
