using System;
using System.IO;

namespace Shy.BuiltinCommands;

public class CdCommand : BuiltinCommand
{
    public override string Name => "cd";

    public override string Description => "changes the current working directory";

    private readonly string _homeDirectory = Environment.GetEnvironmentVariable("HOME");

    public override BuiltinCommandResult Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("cd: missing argument");
            return new BuiltinCommandResult(CommandExecutionResult.Failure, PostCommandAction.None);
        }

        try
        {
            string finalPath;
            var pathArg = args[0];

            if(IsPathAbsolute(pathArg))
            {
                finalPath = pathArg;
            }                
            else
            {
                // If path contains ~, replace it with home directory
                var adjustedPath = ReplaceTildeWithHomeDirectory(pathArg);
                finalPath = Path.Combine(Environment.CurrentDirectory, adjustedPath);
            }

            if(DoesPathExist(finalPath))
            {
                Environment.CurrentDirectory = finalPath;
                return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
            }

            Console.WriteLine($"cd: no such directory");
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

    private string ReplaceTildeWithHomeDirectory(string path)
    {
        if (path.StartsWith('~'))
        {
            return _homeDirectory + path[1..];
        }

        return path;
    }
}