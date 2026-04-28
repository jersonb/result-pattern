namespace MyResult.Tests;

public class ResultTests
{
    [Fact]
    public void Result_Type_default()
    {
        Assert.Equal(0, Result<int>.Empty.Value);
        Assert.Equal("", Result<string>.Empty.Value);
        Assert.NotNull(Result<TestEmpty>.Empty.Value);
        Assert.Empty(Result<List<TestEmpty>>.Empty.Value);
        Assert.Empty(Result<List<TestEmpty>>.Empty.Value);
    }

    [Fact]
    public void Result_implicit_operator_empty()
    {
        Assert.Equal("", Result<string>.Empty);
        Assert.Equal(0, (int)Result<int>.Empty);
        Assert.NotNull(Result<TestEmpty>.Empty);
        Assert.Empty((List<TestEmpty>)Result<List<TestEmpty>>.Empty);
        Assert.Empty((List<TestEmpty>)Result<List<TestEmpty>>.Empty);
    }

    [Fact]
    public void Result_implicit_operator()
    {
        Assert.Equal("Test", Result.Create("Test"));
        Result<string> test = "Test";
        Assert.Equal("Test", test);
    }

    public record TestEmpty;
}