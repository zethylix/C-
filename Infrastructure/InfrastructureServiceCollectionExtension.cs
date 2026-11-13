using Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<Database>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        return services;
    }
}