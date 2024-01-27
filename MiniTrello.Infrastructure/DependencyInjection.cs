using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniTrello.Domain.Ticket.Projections.TicketDetails;
using MiniTrello.Infrastructure.Persistence;
using MiniTrello.Infrastructure.Persistence.Repositories;

namespace MiniTrello.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventStore, InMemoryEventStore>();
        services.AddSingleton<ITicketDetailsProjectionRepository, InMemoryTicketDetailsProjectionRepository>();

        services.AddDbContext<MiniTrelloDbContext>(
            opts => opts.UseSqlServer(configuration.GetConnectionString("MiniTrelloSqlServer"), 
            b => b.MigrationsAssembly("MiniTrello.Infrastructure")));

        return services;
    }
}
