using Application.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore;

public class TodoRepository(TodoContext context) :ITodoRepository
{
    public async Task<Todo?> GetById(int id)
    {
        return  await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async  Task<List<Todo>> GetAll()
    {
        return await context.Todos.ToListAsync();
    }

    public async Task<Todo> Create(Todo todo)
    {
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
        return new Todo
        {
            Id = todo.Id,
            Title = todo.Title,
            Done = todo.Done
        };
    }

    public async Task<bool> Update(Todo todo)
    {
        
        var tracked=await context.Todos.FindAsync(todo.Id);
        if (tracked == null)
            return false;
        tracked.Title = todo.Title;
        tracked.Done=todo.Done;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        return await context.Todos
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync() > 0;
    }
}