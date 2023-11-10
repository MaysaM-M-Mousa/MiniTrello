using Microsoft.Extensions.DependencyInjection;

namespace MiniTrello.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            var assembly = typeof(DependencyInjection).Assembly;
            config.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }
}
