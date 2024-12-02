namespace ToDoList.WebApi.Controllers;

using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController(IRepository<ToDoItem> repository) : ControllerBase


{

    private readonly IRepository<ToDoItem> repository = repository;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ToDoItemsCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            await repository.CreateAsync(item);
            var dto = ToDoItemGetResponseDto.FromDomain(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { toDoItemId = item.ToDoItemId }, dto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemGetResponseDto>>> ReadAsync()
    {
        try
        {
            var items = await repository.GetAllAsync();
            var response = items.Select(ToDoItemGetResponseDto.FromDomain).ToList();

            if (!response.Any())
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    [ActionName(nameof(GetByIdAsync))] //bohuzel tady je to potreba takto napsat kvuli navratove hodnote CreateAsync, proc nemam tuseni, nelibi se mu to Async v nazvu - viz debata v sekci otazky
    public async Task<IActionResult> GetByIdAsync(int toDoItemId)
    {
        try
        {
            var item = await repository.GetByIdAsync(toDoItemId);

            return item == null ? NotFound() : Ok(ToDoItemGetResponseDto.FromDomain(item));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public async Task<IActionResult> UpdateByIdAsync(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var item = await repository.GetByIdAsync(toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = request.Name;
            item.Description = request.Description;
            item.IsCompleted = request.IsCompleted;
            item.Category = request.Category;

            await repository.UpdateAsync(item);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public async Task<IActionResult> DeleteByIdAsync(int toDoItemId)
    {
        try
        {
            var item = await repository.GetByIdAsync(toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(item);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
