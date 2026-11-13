using Application.Abstractions;

namespace Application.Todos.GetById;

public sealed class UseCaseGetByIdTodo(ITodoRepository todoRepository) : IQueryHandler<GetByIdTodoQuery, TodoDto?>
{
    public async Task<TodoDto?> HandleAsync(GetByIdTodoQuery query)
    {
        var todo = await todoRepository.GetById(query.Id);
        return todo is null ? null : TodoDto.From(todo);
    }
}