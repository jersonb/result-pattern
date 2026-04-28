namespace MyResult;

public class Result<TValue>
{
    public static Result<TValue> Empty => new();

    public TValue Value { get; }
    public bool IsSuccess { get; }
    public bool IsFail => !IsSuccess;
    public bool IsNull => Value == null!;
    public ErrorResult Error { get; }

    private Result()
    {
        Value = typeof(TValue) switch
        {
            { IsPrimitive: true } => default!,
            var t when t == typeof(string) => (TValue)(object)string.Empty,
            { } => Activator.CreateInstance<TValue>() ?? throw new InvalidCastException(),
        };
        IsSuccess = true;
        Error = ErrorResult.Empty;
    }

    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = ErrorResult.Empty;
    }

    private Result(ErrorResult error)
    {
        IsSuccess = false;
        Value = default!;
        Error = error;
    }

    public static implicit operator TValue(Result<TValue> value)
        => value.Value;

    public static implicit operator Result<TValue>(TValue value)
        => new(value);

    public static implicit operator bool(Result<TValue> value)
        => value is { IsNull: false, IsSuccess: true };

    public static implicit operator Result<TValue>(ErrorResult error)
        => new(error);
}

public static partial class Result
{
    public static Result<TValue> Empty<TValue>() => Result<TValue>.Empty;

    public static Result<TValue> Create<TValue>(TValue value) => value;

    public static Result<TValue> CreateError<TValue>() => ErrorResult.Empty;

    public static Result<TValue> CreateError<TValue>(string code) => ErrorResult.CreateError(code);

    public static Result<TValue> CreateError<TValue>(string code, string message) => ErrorResult.CreateError(code, message);

    public static Result<TValue> CreateError<TValue>(string code, List<string> messages) => ErrorResult.CreateError(code, messages);
}