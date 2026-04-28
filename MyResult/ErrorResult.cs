namespace MyResult;

public class ErrorResult
{
    public static ErrorResult CreateError(string code, List<string> messages) => new(code, messages);

    public static ErrorResult CreateError(string code, string message) => new(code, message);

    public static ErrorResult CreateError(string code) => new(code);

    public static ErrorResult Empty => new();

    private ErrorResult()
    {
        Code = string.Empty;
        Messages = [];
    }

    private ErrorResult(string code, List<string> messages)
    {
        Code = code;
        Messages = messages;
    }

    private ErrorResult(string code, string message)
    {
        Code = code;
        Messages = [message];
    }

    private ErrorResult(string code)
    {
        Code = code;
        Messages = [];
    }

    public string Code { get; }
    public List<string> Messages { get; private set; }

    public ErrorResult AddMessage(string message)
    {
        Messages.Add(message);
        return this;
    }

    public ErrorResult AddException(Exception exception)
    {
        Messages.Add(exception.Message);
        return this;
    }

    public static implicit operator ErrorResult(string code)
        => new(code);
}

public static class ErrorResultExtensions
{
    public static ErrorResult AddMessage(this ErrorResult errorResult, string message)
    {
        errorResult.AddMessage(message);
        return errorResult;
    }

    public static Result<TValue> AddMessage<TValue>(this Result<TValue> result, string message)
    {
        result.Error.AddMessage(message);
        return result;
    }

    public static ErrorResult AddException(this ErrorResult errorResult, Exception exception)
    {
        errorResult.AddMessage(exception.Message);
        return errorResult;
    }

    public static Result<TValue> AddException<TValue>(this Result<TValue> result, Exception exception)
    {
        result.Error.AddException(exception);
        return result;
    }
}