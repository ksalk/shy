namespace Shy.BuiltinCommands;

public class ExitCommand : BuiltinCommand
{
    public override string Name => "exit";

    public override string Description => "exits shell session";

    public override BuiltinCommandResult Execute(string[] args)
    {
        return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.ExitShell);
    }
}