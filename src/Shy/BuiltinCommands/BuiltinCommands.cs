using System.Linq;

namespace Shy.BuiltinCommands;

public static class BuiltinCommandsRegistry
{
    public static readonly BuiltinCommand[] Commands =
    [
        new ExitCommand(),
        new EchoCommand(),
        new TypeCommand()
    ];

    public static BuiltinCommand? GetCommandByName(string name)
    {
        return Commands.FirstOrDefault(c => string.Equals(c.Name, name));
    }
}