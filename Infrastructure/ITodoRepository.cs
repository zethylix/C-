using Domain;

namespace Infrastructure;

public interface ITodoRepository
{
    Todo? GetById(int id);
    List<Todo> GetAll();
    Todo Create(Todo todo);
    bool Update(Todo todo);
    bool Delete(int id);
}