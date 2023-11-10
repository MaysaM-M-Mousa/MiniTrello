using Microsoft.Extensions.DependencyInjection;
using MiniTrello.Application.Common.Interfaces;
using MiniTrello.Infrastructure.Persistence.Repositories;

namespace MiniTrello.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ITicketRepository, InMemoryTicketRepository>();
        services.AddSingleton<ITicketDetailsProjectionRepository, InMemoryTicketDetailsProjectionRepository>();

        return services;
    }
}
