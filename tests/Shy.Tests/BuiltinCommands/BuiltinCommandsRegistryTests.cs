
using Shy.BuiltinCommands;

public class BuiltinCommandsRegistryTests
{
    [Theory]
    [InlineData("exit")]
    [InlineData("echo")]
    [InlineData("type")]
    public void GetCommandByName_ShouldReturnCommand_WhenNameExists(string name)
    {
        var command = BuiltinCommandsRegistry.GetCommandByName(name);

        Assert.NotNull(command);
        Assert.Equal(name, command.Name);
    }

    [Fact]
    public void GetCommandByName_ShouldReturnNull_WhenNameDoesNotExist()
    {
        var command = BuiltinCommandsRegistry.GetCommandByName("nonexistent");

        Assert.Null(command);
    }
}
