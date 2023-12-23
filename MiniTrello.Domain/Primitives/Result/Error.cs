namespace MiniTrello.Domain.Primitives.Result;

public sealed record Error(string Code, string? Message = null);