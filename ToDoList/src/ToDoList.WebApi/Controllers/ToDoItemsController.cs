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
    public IActionResult Create(ToDoItemsCreateRequestDto request)
    {

        var item = request.ToDomain();
        try
        {
            repository.Create(item);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }


        return CreatedAtAction(nameof(GetById), new { toDoItemId = item.ToDoItemId }, item);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        try
        {

            var response = repository.GetAll().Select(ToDoItemGetResponseDto.FromDomain).ToList();

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
    public IActionResult GetById(int toDoItemId)
    {

        try
        {
            var item = repository.GetById(toDoItemId);

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
            var item = repository.GetById(toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            var updatedItem = request.ToDomain();
            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.IsCompleted = updatedItem.IsCompleted;
            item.Category = updatedItem.Category;

            repository.Update(item);
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
            var item = repository.GetById(toDoItemId);
            if (item == null)
            {
                return NotFound();

            }

            repository.Delete(item);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
