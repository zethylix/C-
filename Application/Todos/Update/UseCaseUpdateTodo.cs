using Application.Abstractions;
using Domain;

namespace Application.Todos.Update;

public sealed class UseCaseUpdateTodo(ITodoRepository todoRepository) : ICommandHandler<UpdateTodoCommand, bool>
{
    public async Task<bool> HandleAsync(UpdateTodoCommand command)
    {
        return await todoRepository.Update(new Todo
        {
            Id = command.Id,
            Title = command.Title,
            Done = command.Done
        });
    }
}