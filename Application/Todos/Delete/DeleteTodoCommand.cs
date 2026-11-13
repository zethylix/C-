using Application.Abstractions;

namespace Application.Todos.Delete;

public sealed record DeleteTodoCommand(int Id) : ICommand<bool>;