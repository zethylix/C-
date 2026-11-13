using Application.Todos.Create;
using Application.Todos.Delete;
using Application.Todos.GetAll;
using Application.Todos.GetById;
using Application.Todos.Update;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(
    UseCaseGetAllTodos useCaseGetAllTodos,
    UseCaseGetByIdTodo useCaseGetByIdTodo,
    UseCaseCreateTodo useCaseCreateTodo,
    UseCaseUpdateTodo useCaseUpdateTodo,
    UseCaseDeleteTodo useCaseDeleteTodo) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(int id)
    {
        var todo = await useCaseGetByIdTodo.HandleAsync(new GetByIdTodoQuery(id));
        if (todo is null)
            return NotFound();
        return Ok(todo);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await useCaseGetAllTodos.HandleAsync(new GetAllTodosQuery());
        return Ok(result); // 200
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateTodoCommand command)
    {
        if (string.IsNullOrEmpty(command.Title))
            return BadRequest("Title can not be null!");

        var created = await useCaseCreateTodo.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateTodoCommand command)
    {
        if (await useCaseUpdateTodo.HandleAsync(command))
            return NoContent();
        return NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (await useCaseDeleteTodo.HandleAsync(new DeleteTodoCommand(id)))
            return NoContent();
        return NotFound();
    }
}