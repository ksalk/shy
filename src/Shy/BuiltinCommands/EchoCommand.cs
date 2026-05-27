using System;

namespace Shy.BuiltinCommands;

public class EchoCommand : BuiltinCommand
{
    public override string Name => "echo";

    public override string Description => "echoes text back to user";

    public override BuiltinCommandResult Execute(string[] args)
    {
        Console.WriteLine(string.Join(' ', args));
        
        return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
    }
}