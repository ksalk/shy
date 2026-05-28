using System;

namespace Shy.BuiltinCommands;

public static class BuiltinCommandsRegistry
{
    public static readonly BuiltinCommand[] Commands =
    [
        new ExitCommand(),
        new EchoCommand(),
        new TypeCommand(),
        new PwdCommand(),
        new CdCommand()
    ];

    public static BuiltinCommand? GetCommandByName(string name)
    {
        foreach (var cmd in Commands)
        {
            if (string.Equals(cmd.Name, name, StringComparison.Ordinal))
                return cmd;
        }
        return null;
    }
}