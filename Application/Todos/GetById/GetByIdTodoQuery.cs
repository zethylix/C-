using Application.Abstractions;

namespace Application.Todos.GetById;

public sealed record GetByIdTodoQuery(int Id) : IQuery<TodoDto?>;