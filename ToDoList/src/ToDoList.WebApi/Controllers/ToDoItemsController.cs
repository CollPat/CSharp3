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
            /*
            Prehlednejsi formatovani, C# sice formatovani bez {} zavorek u jednoduchych if statementu umoznuje, ovsem good practise je podle me to spise psat takto
            hlavni duvody:
            1. prehlednost - kdyz projizdis ocima kod, tak je jasne poznat blok kodu v ifu
            2. "bezpecnost" - pod timto se muze vlivem nepozornosti schovat spousta bugu :) (napr kdyz pridam dalsi radek do ifu tak si nemusim v rychlosti uvedomit ze
            uz nespada do if statement) -> predchazim potencionalnim bugum

            Ale je to takove kontroverzni tema, nekdo to miluje, nekdo ne, nechvavam na tobe. Tuto cast kodu jsem zmenil :)
            */
            if (items == null || items.Count == 0) //takto zadani neznelo, NotFound() vracime pouze pokud je items == null
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

            //muzeme to zkratit na mensi pocet radku pomoci ternarniho operatoru ?:
            //jak to vypada:
            //podminka ? splnena podminka : nesplena podminka
            //return (items is null) ?  NotFound() : Ok(ToDoItemGetResponseDto.FromDomain(item));

            if (item == null) //opet {} ale nechavam na tobe :)
                return NotFound(); // 404 Not Found

            var response = ToDoItemGetResponseDto.FromDomain(item);
            return Ok(response); // 200 OK
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //odsazeni je spatne, pravdepodobne to vzniklo prekopirovavanim odhaduju :) Kompiler to prechrousta ale spatne se to cte
       try
    {
        var index = items.FindIndex(x => x.ToDoItemId == toDoItemId);
        if (index == -1) //same story with {} :)
            return NotFound(); // 404 Not Found

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
        //opet odsazeni :)
        try
    {
        var item = items.Find(x => x.ToDoItemId == toDoItemId);
        if (item == null)
            return NotFound(); // 404 Not Found

        items.Remove(item);
        return NoContent(); // 204 No Content
    }
    catch (Exception ex)
    {
        return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500 Internal Server Error
    }
    }
}
