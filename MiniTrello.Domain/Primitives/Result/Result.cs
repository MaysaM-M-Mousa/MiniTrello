namespace MiniTrello.Domain.Primitives.Result;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    private Result(bool isSuccess, Error? error = null)
    {
        if ((isSuccess && error != null) || (!isSuccess && error == null))
        {
            throw new InvalidOperationException("Invalid Result status");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);

    public static Result Failure(Error error) => new(false, error);
}
