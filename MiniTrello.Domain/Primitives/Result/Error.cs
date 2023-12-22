namespace MiniTrello.Domain.Primitives.Result;

public sealed record Error(string Code, string? Message = null)
{
    public static implicit operator Result(Error error) => Result.Failure(error);
}