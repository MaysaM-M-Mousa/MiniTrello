namespace MiniTrello.Domain.Exceptions;

public class MiniTrelloException : Exception
{
    public int StatusCode { get; set; }

    public MiniTrelloException(string message) : base(message)
    {

    }
}
