namespace MyResult.Tests;

public class ResultWithError
{
    [Fact]
    public void Result_WithError_ShouldHaveError_exception()
    {
        var message = Guid.NewGuid().ToString();
        var exception = new Exception(message);

        var result = Result
            .CreateError<string>()
            .AddException(exception);

        Assert.False(result.IsSuccess);
        Assert.Empty(result.Error.Code);
        Assert.Equal(message, Assert.Single(result.Error.Messages));

        Assert.IsType<Result<string>>(result);
    }

    [Fact]
    public void Result_WithError_ShouldHaveError_only_message()
    {
        var message = Guid.NewGuid().ToString();
        var result = Result
            .CreateError<string>()
            .AddMessage(message);

        Assert.False(result.IsSuccess);
        Assert.Empty(result.Error.Code);
        Assert.Equal(message, Assert.Single(result.Error.Messages));

        Assert.IsType<Result<string>>(result);
    }

    [Fact]
    public void Result_WithError_ShouldHaveError_only_code()
    {
        var code = Guid.NewGuid().ToString();
        var result = Result
            .CreateError<string>(code);

        Assert.False(result.IsSuccess);
        Assert.Equal(code, result.Error.Code);
        Assert.Empty(result.Error.Messages);
        Assert.IsType<Result<string>>(result);
    }

    [Fact]
    public void Result_WithError_ShouldHaveError_code_and_single_message()
    {
        var code = Guid.NewGuid().ToString();
        var message = Guid.NewGuid().ToString();
        var result = Result
            .CreateError<string>(code, message);

        Assert.False(result.IsSuccess);
        Assert.Equal(code, result.Error.Code);
        Assert.Equal(message, Assert.Single(result.Error.Messages));
        Assert.IsType<Result<string>>(result);
    }

    [Fact]
    public void Result_WithError_ShouldHaveError_code_and_many_message()
    {
        var code = Guid.NewGuid().ToString();
        var message1 = Guid.NewGuid().ToString();
        var message2 = Guid.NewGuid().ToString();
        var result = Result
            .CreateError<string>(code, message1)
            .AddMessage(message2);

        Assert.False(result.IsSuccess);
        Assert.Equal(code, result.Error.Code);
        Assert.Contains(message1, result.Error.Messages);
        Assert.Contains(message2, result.Error.Messages);
        Assert.IsType<Result<string>>(result);
    }
}