using Application.Abstractions;
using Infrastructure.ADO;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TodoRepository = Infrastructure.EFCore.TodoRepository;

namespace Infrastructure;

public static class InfrastructureServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<Database>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddDbContext<TodoContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("TodoDb"))
            );
        return services;
    }
}