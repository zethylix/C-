using Application.Abstractions;

namespace Application.Todos.Delete;

public sealed class UseCaseDeleteTodo(ITodoRepository todoRepository) : ICommandHandler<DeleteTodoCommand, bool>
{
    public async Task<bool> HandleAsync(DeleteTodoCommand command)
    {
        return await todoRepository.Delete(command.Id);
    }
}