namespace MiniTrello.Domain.Exceptions;

public class TicketAlreadyUnassignedException : MiniTrelloValidationException
{
    private const string DefaultMessage = "The ticket already unassigned";

    public TicketAlreadyUnassignedException() : base(DefaultMessage)
    {
    }

    public TicketAlreadyUnassignedException(string message) : base(message)
    {
    }
}
