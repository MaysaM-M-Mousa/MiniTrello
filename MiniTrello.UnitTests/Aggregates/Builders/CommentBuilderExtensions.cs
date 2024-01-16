using MiniTrello.Domain.Ticket.Comment;

namespace MiniTrello.UnitTests.Aggregates.Builders;

public static class CommentBuilderExtensions
{
    public static Comment BuildNewlyAddedComment(this CommentBuilder commentBuilder) =>
        commentBuilder
        .AddCommentAddedEvent()
        .Build();

    public static Comment BuildDeletedComment(this CommentBuilder commentBuilder) =>
        commentBuilder
        .AddCommentAddedEvent()
        .AddCommentDeletedEvent()
        .Build();
}
