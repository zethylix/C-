using Application.Abstractions;

namespace Application.Todos.GetAll;

public sealed record GetAllTodosQuery : IQuery<List<TodoDto>> //bool?Overdue,string?Search
{
}