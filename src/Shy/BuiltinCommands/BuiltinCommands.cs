namespace Shy.BuiltinCommands;

public static class BuiltinCommandsRegistry
{
    public static readonly BuiltinCommand[] Commands = new BuiltinCommand[]
    {
        new ExitCommand(),
        new EchoCommand(),
        new TypeCommand()
    };
}