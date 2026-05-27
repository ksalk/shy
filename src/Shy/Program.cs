using System;

namespace Shy;

public class Program
{
    public static void Main(string[] args)
    {
        while(true)
        {
            // PROMPT
            Console.Write("shy> ");

            // READ
            var command = Console.ReadLine();
            if(string.IsNullOrEmpty(command))
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

            Console.WriteLine($"{command}: command not found");

            // PRINT

            // LOOP
        }
    }
}