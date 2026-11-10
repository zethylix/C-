using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(ITodoRepository todoRepository) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(int id)
    {
        var todo = await todoRepository.GetById(id);
        if (todo is null)
            return NotFound();
        return Ok(todo);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await todoRepository.GetAll();
        return Ok(result); // 200
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(Todo todo)
    {
        if (string.IsNullOrEmpty(todo.Title))
            return BadRequest("Title can not be null!");

        var created = await todoRepository.Create(todo);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Todo todo)
    {
        if (await todoRepository.Update(todo))
            return NoContent();
        return NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (await todoRepository.Delete(id))
            return NoContent();
        return NotFound();
    }
}