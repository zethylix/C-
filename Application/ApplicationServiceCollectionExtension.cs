using Application.Todos.Create;
using Application.Todos.Delete;
using Application.Todos.GetAll;
using Application.Todos.GetById;
using Application.Todos.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UseCaseGetAllTodos>();
        services.AddScoped<UseCaseGetByIdTodo>();
        services.AddScoped<UseCaseCreateTodo>();
        services.AddScoped<UseCaseUpdateTodo>();
        services.AddScoped<UseCaseDeleteTodo>();
        return services;
    }
}