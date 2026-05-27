
using Shy.BuiltinCommands;

[Collection("Console")]
public class EchoCommandTests
{
    [Fact]
    public void Execute_ShouldReturnSuccess()
    {
        var command = new EchoCommand();

        var result = command.Execute(["hello"]);

        Assert.Equal(CommandExecutionResult.Success, result.ExecutionResult);
        Assert.Equal(PostCommandAction.None, result.PostAction);
    }

    [Fact]
    public void Execute_ShouldWriteJoinedArgsToConsole()
    {
        var command = new EchoCommand();
        var output = new StringWriter();
        Console.SetOut(output);

        command.Execute(["hello", "world"]);

        Assert.Equal("hello world" + Environment.NewLine, output.ToString());
    }

    [Fact]
    public void Execute_ShouldWriteEmptyLine_WhenNoArgs()
    {
        var command = new EchoCommand();
        var output = new StringWriter();
        Console.SetOut(output);

        command.Execute([]);

        Assert.Equal(Environment.NewLine, output.ToString());
    }
}
