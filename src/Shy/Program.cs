using System;
using System.Diagnostics;
using System.Linq;
using Shy.BuiltinCommands;

namespace Shy;

public class Program
{
    public static void Main(string[] args)
    {
        if (OperatingSystem.IsWindows())
        {
            Console.WriteLine("Sorry, windows is not supported :(");
            return;
        }

        while (true)
        {
            // PROMPT USER
            PrintPrompt();

            // READ HIS INPUT
            var (commandName, commandArgs) = ReadUserInput();
            
            // EVAL

            // check if command is a builtin command
            var builtinCommand = BuiltinCommandsRegistry.GetCommandByName(commandName);
            if (builtinCommand != null)
            {
                var commandResult = builtinCommand.Execute(commandArgs);

                if (commandResult.PostAction == PostCommandAction.ExitShell)
                    break;
                else if (commandResult.PostAction == PostCommandAction.None)
                    continue;

                // should not be hit
                continue;
            }

            // check if command is an executable in PATH
            var executablePath = ExecutableProvider.FindExecutablePathByName(commandName);
            if (!string.IsNullOrWhiteSpace(executablePath))
            {
                RunProgram(commandName, commandArgs, executablePath);

                continue;
            }

            // report command not found
            Console.WriteLine($"{commandName}: command not found");
        }
    }

    private static void PrintPrompt() => Console.Write("shy> ");

    private static (string command, string[] args) ReadUserInput()
    {
        var userCommand = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(userCommand))
        {
            return (string.Empty, []);
        }

        // TODO: tokenize command and arguments
        var commandTokens = userCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return (commandTokens[0], commandTokens[1..]);
    }

    private static void RunProgram(string command, string[] args, string executablePath)
    {
        var processStartInfo = new ProcessStartInfo(executablePath, args)
        {
            RedirectStandardOutput = true
        };

        // TODO: read up on this stuff
        Process? process = Process.Start(processStartInfo);
        if (process == null)
        {
            Console.WriteLine($"{command}: error executing command");
            return;
        }

        using var processOutput = process.StandardOutput;
        while (!processOutput.EndOfStream)
        {
            Console.WriteLine(processOutput.ReadToEnd());
        }
    }
}