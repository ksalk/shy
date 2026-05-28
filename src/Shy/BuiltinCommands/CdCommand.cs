using System;
using System.IO;

namespace Shy.BuiltinCommands;

public class CdCommand : BuiltinCommand
{
    public override string Name => "cd";

    public override string Description => "changes the current working directory";

    public override BuiltinCommandResult Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("cd: missing argument");
            return new BuiltinCommandResult(CommandExecutionResult.Failure, PostCommandAction.None);
        }

        try
        {
            var newPath = args[0];
            if (IsPathAbsolute(newPath) && DoesPathExist(newPath))
            {
                Environment.CurrentDirectory = newPath;
                return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
            }

            var combinedPath = Path.Combine(Environment.CurrentDirectory, newPath);
            if (DoesPathExist(combinedPath))
            {
                Environment.CurrentDirectory = combinedPath;
                return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
            }

            Console.WriteLine($"cd: no such directory: {newPath}");
            return new BuiltinCommandResult(CommandExecutionResult.Failure, PostCommandAction.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"cd: {ex.Message}");
            return new BuiltinCommandResult(CommandExecutionResult.Failure, PostCommandAction.None);
        }
    }

    private bool IsPathAbsolute(string path) => Path.IsPathRooted(path);

    private bool DoesPathExist(string path) => Directory.Exists(path);
}