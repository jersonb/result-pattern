namespace MyResult.Tests;

public class ErrorResultTests
{
    [Fact]
    public void CreateError_WithCodeOnly_ShouldInitializeProperties()
    {
        // Arrange
        string code = "ERR001";
        // Act
        var errorResult = ErrorResult.CreateError(code);
        // Assert
        Assert.Equal(code, errorResult.Code);
        Assert.Empty(errorResult.Messages);
    }

    [Fact]
    public void CreateError_WithCodeAndMessage_ShouldInitializeProperties()
    {
        // Arrange
        string code = "ERR002";
        string message = "An error occurred.";
        // Act
        var errorResult = ErrorResult.CreateError(code, message);
        // Assert
        Assert.Equal(code, errorResult.Code);
        Assert.Single(errorResult.Messages);
        Assert.Equal(message, errorResult.Messages[0]);
    }

    [Fact]
    public void CreateError_WithCodeAndMessages_ShouldInitializeProperties()
    {
        // Arrange
        var code = "ERR003";
        List<string> messages = new() { "First error message.", "Second error message." };
        // Act
        var errorResult = ErrorResult.CreateError(code, messages);
        // Assert
        Assert.Equal(code, errorResult.Code);
        Assert.Equal(messages.Count, errorResult.Messages.Count);
        Assert.Equal(messages[0], errorResult.Messages[0]);
        Assert.Equal(messages[1], errorResult.Messages[1]);
    }

    [Fact]
    public void AddMessage_ShouldAddMessageToMessagesList()
    {
        // Arrange
        var errorResult = ErrorResult.CreateError("ERR004");
        var message = "New error message.";
        // Act
        errorResult.AddMessage(message);
        // Assert
        Assert.Single(errorResult.Messages);
        Assert.Equal(message, errorResult.Messages[0]);
    }

    [Fact]
    public void AddException_ShouldAddExceptionMessageToMessagesList()
    {
        // Arrange
        var errorResult = ErrorResult.CreateError("ERR005");
        var exceptionMessage = "An exception occurred.";
        var exception = new Exception(exceptionMessage);
        // Act
        errorResult.AddException(exception);
        // Assert
        Assert.Single(errorResult.Messages);
        Assert.Equal(exceptionMessage, errorResult.Messages[0]);
    }
}