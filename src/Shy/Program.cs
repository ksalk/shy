using System;
using System.IO;
using Microsoft.VisualBasic;

namespace Shy;

public class Program
{
    public static void Main(string[] args)
    {
        if(OperatingSystem.IsWindows())
        {
            Console.WriteLine("Sorry, windows is not supported :(");
            return;
        }
            
        while (true)
        {
            // PROMPT
            Console.Write("shy> ");

            // READ
            var command = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(command))
            {
                continue;
            }

            // EVAL
            if (command == "exit")
            {
                break;
            }

            if (command.StartsWith("echo "))
            {
                var echoMessage = command.Substring(5);
                Console.WriteLine(echoMessage);
                continue;
            }

            if (command.StartsWith("type "))
            {
                var typeCommand = command.Substring(5);
                if (typeCommand.Equals("exit"))
                {
                    Console.WriteLine("exit is a shell builtin");
                    continue;
                }
                else if (typeCommand.StartsWith("echo "))
                {
                    Console.WriteLine("echo is a shell builtin");
                    continue;
                }
                else if (typeCommand.StartsWith("type "))
                {
                    Console.WriteLine("type is a shell builtin");
                    continue;
                }

                var executableFile = FindExecutableByName(typeCommand);
                if (!string.IsNullOrWhiteSpace(executableFile))
                {
                    Console.WriteLine($"{typeCommand} is {executableFile}");
                    continue;
                }

                Console.WriteLine($"{typeCommand}: not found");
            }

            Console.WriteLine($"{command}: command not found");

            // PRINT

            // LOOP
        }
    }

    private static string? FindExecutableByName(string commandName)
    {
        const string pathEnvVarName = "PATH";
        var pathEnvVar = Environment.GetEnvironmentVariable(pathEnvVarName);

        if (string.IsNullOrWhiteSpace(pathEnvVar))
        {
            return null;
        }

        var pathDirectories = pathEnvVar.Split(Path.PathSeparator);

        foreach (var pathDirectory in pathDirectories)
        {
            var filePath = Path.Combine(pathDirectory, commandName);
            if (!Path.Exists(filePath))
                continue;

            // TODO: possibly check other execute modes
            // TODO: this is linux/macos only
            var unixFileMode = File.GetUnixFileMode(filePath);
            if (!unixFileMode.HasFlag(UnixFileMode.UserExecute))
                continue;

            return filePath;
        }

        return null;
    }
}