namespace MiniTrello.Domain.Exceptions;

public class MiniTrelloNotFoundException : MiniTrelloException
{
    public MiniTrelloNotFoundException(string id) : base($"Entity with Id {id} could not be found!")
    {
    }
}
