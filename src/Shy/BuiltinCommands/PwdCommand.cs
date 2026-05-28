using System;

namespace Shy.BuiltinCommands;

public class PwdCommand : BuiltinCommand
{
    public override string Name => "pwd";

    public override string Description => "prints the current working directory";

    public override BuiltinCommandResult Execute(string[] args)
    {
        Console.WriteLine(Environment.CurrentDirectory);
        
        return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
    }
}