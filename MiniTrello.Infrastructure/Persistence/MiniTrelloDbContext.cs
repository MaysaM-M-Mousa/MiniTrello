using Microsoft.EntityFrameworkCore;

namespace MiniTrello.Infrastructure.Persistence;

internal class MiniTrelloDbContext : DbContext
{
    public MiniTrelloDbContext(DbContextOptions<MiniTrelloDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniTrelloDbContext).Assembly);
    }
}
