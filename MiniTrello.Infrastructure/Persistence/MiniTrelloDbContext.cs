using Microsoft.EntityFrameworkCore;
using MiniTrello.Domain.Ticket;

namespace MiniTrello.Infrastructure.Persistence;

internal class MiniTrelloDbContext : DbContext
{
    public MiniTrelloDbContext(DbContextOptions<MiniTrelloDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniTrelloDbContext).Assembly);
    }

    DbSet<Comment> Comment { get; set; } = null!;
}
