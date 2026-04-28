namespace MyResult.Tests;

public class ResultWithError
{
    [Fact]
    public void Result_WithError_ShouldHaveError()
    {
        var result = Result
            .CreateError("")
            .AddMessage("Paranauê");

        Assert.False(result.IsSuccess);
        Assert.Single(result.Error.Messages);
    }
}