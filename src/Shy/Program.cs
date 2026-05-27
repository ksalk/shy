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
            // PROMPT
            Console.Write("shy> ");

            // READ
            var prompt = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(prompt))
            {
                continue;
            }

            // TODO: tokenize command and arguments
            var promptTokens = prompt.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var command = promptTokens[0];

            // EVAL
            var builtinCommand = BuiltinCommandsRegistry.GetCommandByName(command);
            if (builtinCommand != null)
            {
                var commandResult = builtinCommand.Execute(promptTokens[1..]);

                if (commandResult.PostAction == PostCommandAction.ExitShell)
                    break;
                else if (commandResult.PostAction == PostCommandAction.None)
                    continue;

                // should not be hit
                continue;
            }

            var executablePath = ExecutableProvider.FindExecutablePathByName(command);
            if (!string.IsNullOrWhiteSpace(executablePath))
            {
                var commandArgs = promptTokens[1..];
                RunProgram(command, commandArgs, executablePath);

                continue;
            }

            Console.WriteLine($"{command}: command not found");
        }
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