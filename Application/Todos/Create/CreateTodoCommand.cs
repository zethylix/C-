using Application.Abstractions;

namespace Application.Todos.Create;

public sealed record CreateTodoCommand(string Title, bool Done) : ICommand<TodoDto>;