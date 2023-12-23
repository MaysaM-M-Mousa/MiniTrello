namespace MiniTrello.Domain.Primitives.Result;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected internal Result(bool isSuccess, Error? error = null)
    {
        if ((isSuccess && error != null) || (!isSuccess && error == null))
        {
            throw new InvalidOperationException("Invalid Result status");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);

    public static Result<TSuccess> Success<TSuccess>(TSuccess value) => new Result<TSuccess>(value, true);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TSuccess> Failure<TSuccess>(Error error) => new Result<TSuccess>(default, false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<TSuccess> : Result
{
    private readonly TSuccess _value;

    protected internal Result(TSuccess value, bool isSuccess, Error? error = null) : base(isSuccess, error)
    {
        _value = value;
    }

    public TSuccess Value => IsSuccess ? _value : throw new InvalidOperationException("Can't access success data in failure scenarios!");

    public static implicit operator Result<TSuccess>(TSuccess value) => Success<TSuccess>(value);

    public static implicit operator Result<TSuccess>(Error error) => Failure<TSuccess>(error);
}