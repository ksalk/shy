namespace Shy.BuiltinCommands;

public record struct BuiltinCommandResult(CommandExecutionResult ExecutionResult, PostCommandAction PostAction);

public enum CommandExecutionResult
{
    Success = 0,
    Failure = 1
}

public enum PostCommandAction
{
    None = 0,
    ExitShell = 1
}

public abstract class BuiltinCommand
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract BuiltinCommandResult Execute(string[] args);
}