using Domain;

namespace Infrastructure;

public interface ITodoRepository
{
    Task<Todo?> GetById(int id);
    Task<List<Todo>> GetAll();
    Task<Todo> Create(Todo todo);
    Task<bool> Update(Todo todo);
    Task<bool> Delete(int id);
}