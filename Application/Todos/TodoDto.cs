using Domain;

namespace Application.Todos;

//Data
//Transfer
//Object
public sealed record TodoDto(int Id, string Title, bool Done)
{
    public static TodoDto From(Todo todo)
    {
        return new TodoDto(todo.Id, todo.Title, todo.Done);
    }
}