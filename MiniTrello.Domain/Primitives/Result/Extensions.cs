namespace MiniTrello.Domain.Primitives.Result;

public static class Extensions
{
    public static T Match<T>(
        this Result result,
        Func<T> onSuccess,
        Func<Error, T> OnFailure)
    {
        return result.IsSuccess ? onSuccess() : OnFailure(result.Error);
    } 
}
