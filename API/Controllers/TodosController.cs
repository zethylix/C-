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
    public ActionResult GetById(int id)
    {
        var todo = todoRepository.GetById(id);
        if (todo is null)
            return NotFound();
        return Ok(todo);
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        var result = todoRepository.GetAll();
        return Ok(result); // 200
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Create(Todo todo)
    {
        if (string.IsNullOrEmpty(todo.Title))
            return BadRequest("Title can not be null!");

        var created = todoRepository.Create(todo);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(Todo todo)
    {
        if (todoRepository.Update(todo))
            return NoContent();
        return NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        if (todoRepository.Delete(id))
            return NoContent();
        return NotFound();
    }
}