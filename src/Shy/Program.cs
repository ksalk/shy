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
            var builtinCommand = BuiltinCommandsRegistry.Commands.FirstOrDefault(c => string.Equals(c.Name, command));
            if (builtinCommand != null)
            {
                var commandResult = builtinCommand.Execute(promptTokens[1..]);

                if (commandResult.PostAction == PostCommandAction.ExitShell)
                    break;
                else if(commandResult.PostAction == PostCommandAction.None)
                    continue;

                // should not be hit
                continue;
            }

            var executableCommand = ExecutableProvider.FindExecutableByName(command);
            if (!string.IsNullOrWhiteSpace(executableCommand))
            {
                var commandArgs = promptTokens[1..];
                var processStartInfo = new ProcessStartInfo(executableCommand, commandArgs)
                {
                    RedirectStandardOutput = true
                };

                // TODO: read up on this stuff
                Process? process = Process.Start(processStartInfo);
                if (process == null)
                {
                    Console.WriteLine($"{command}: error executing command");
                    continue;
                }

                using var processOutput = process.StandardOutput;
                while (!processOutput.EndOfStream)
                {
                    Console.WriteLine(processOutput.ReadToEnd());
                }
                continue;
            }

            Console.WriteLine($"{command}: command not found");
        }
    }
}