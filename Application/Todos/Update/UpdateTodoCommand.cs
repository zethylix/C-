using Application.Abstractions;

namespace Application.Todos.Update;

public sealed record UpdateTodoCommand(int Id, string Title, bool Done) : ICommand<bool>;