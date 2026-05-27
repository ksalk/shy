
using Shy.BuiltinCommands;

public class ExitCommandTests
{
    [Fact]
    public void Execute_ShouldReturnExitShellPostAction()
    {
        // Arrange
        var command = new ExitCommand();

        // Act
        var result = command.Execute([]);

        // Assert
        Assert.Equal(CommandExecutionResult.Success, result.ExecutionResult);
        Assert.Equal(PostCommandAction.ExitShell, result.PostAction);
    }

    [Fact]
    public void Execute_ShouldReturnSuccessExecutionResult_WhenArgumentsAreProvided()
    {
        // Arrange
        var command = new ExitCommand();

        // Act
        var result = command.Execute(["some", "args"]);

        // Assert
        Assert.Equal(CommandExecutionResult.Success, result.ExecutionResult);
    }
}