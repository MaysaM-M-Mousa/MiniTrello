namespace MiniTrello.Contracts.Common;

public class ListResponse<T>
{
    public List<T> Entities { get; set; }

    public int Count => Entities.Count;
}
