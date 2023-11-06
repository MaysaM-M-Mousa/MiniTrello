namespace MiniTrello.Domain.Exceptions;

public class MiniTrelloValidationException : MiniTrelloException
{
    public MiniTrelloValidationException(string message) : base(message)
    {
        StatusCode = 400; 
    }
}
