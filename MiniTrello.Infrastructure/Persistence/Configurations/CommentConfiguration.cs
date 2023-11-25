using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniTrello.Domain.Ticket;

namespace MiniTrello.Infrastructure.Persistence.Configurations;

internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.TicketId);

        builder
            .Property(x => x.Text)
            .HasMaxLength(1024);
    }
}
