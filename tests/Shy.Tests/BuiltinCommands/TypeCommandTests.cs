
using Shy.BuiltinCommands;

[Collection("Console")]
public class TypeCommandTests
{
    [Fact]
    public void Execute_ShouldReturnSuccess()
    {
        var command = new TypeCommand();

        var result = command.Execute(["exit"]);

        Assert.Equal(CommandExecutionResult.Success, result.ExecutionResult);
        Assert.Equal(PostCommandAction.None, result.PostAction);
    }

    [Fact]
    public void Execute_ShouldPrintBuiltinMessage_WhenCommandIsBuiltin()
    {
        var command = new TypeCommand();
        var output = new StringWriter();
        Console.SetOut(output);

        command.Execute(["exit"]);

        Assert.Contains("exit is a shell builtin", output.ToString());
    }

    [Fact]
    public void Execute_ShouldPrintNotFound_WhenCommandDoesNotExist()
    {
        var command = new TypeCommand();
        var output = new StringWriter();
        Console.SetOut(output);

        command.Execute(["nonexistent_command_xyz"]);

        Assert.Contains("nonexistent_command_xyz: not found", output.ToString());
    }

    [Fact]
    public void Execute_ShouldDoNothing_WhenNoArgs()
    {
        var command = new TypeCommand();
        var output = new StringWriter();
        Console.SetOut(output);

        var result = command.Execute([]);

        Assert.Equal(CommandExecutionResult.Success, result.ExecutionResult);
        Assert.Equal("", output.ToString());
    }
}
