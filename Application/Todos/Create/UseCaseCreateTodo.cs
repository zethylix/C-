using Application.Abstractions;
using Domain;

namespace Application.Todos.Create;

public sealed class UseCaseCreateTodo(ITodoRepository todoRepository) : ICommandHandler<CreateTodoCommand, TodoDto>
{
    public async Task<TodoDto> HandleAsync(CreateTodoCommand command)
    {
        var todo = await todoRepository.Create(new Todo
        {
            Title = command.Title,
            Done = command.Done
        });
        return TodoDto.From(todo);
    }
}