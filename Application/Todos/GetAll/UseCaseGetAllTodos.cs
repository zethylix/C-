using Application.Abstractions;

namespace Application.Todos.GetAll;

public class UseCaseGetAllTodos(ITodoRepository todoRepository)
    : IQueryHandler<GetAllTodosQuery, List<TodoDto>> // Récupérer les données de la db
{
    public async Task<List<TodoDto>> HandleAsync(GetAllTodosQuery query)
    {
        var todos = await todoRepository.GetAll();
        return todos.ConvertAll(TodoDto.From); // Convertit tous les éléments grâce à la méthode

        // var output = new List<TodoDto>();

        // foreach (var todo in todos)
        // {
        //     output.Add(TodoDto.From(todo));
        // }

        // return output;
    }
}