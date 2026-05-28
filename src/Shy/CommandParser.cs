using System;

namespace Shy;

public static class CommandParser
{
    public static CommandParseResult Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return CommandParseResult.Empty();
        }

        // TODO: tokenize command and arguments
        var commandTokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return new CommandParseResult(commandTokens[0], commandTokens[1..]);
    }
}

public class CommandParseResult
{
    public string CommandName { get; }
    public string[] Arguments { get; }

    public CommandParseResult(string commandName, string[] arguments)
    {
        CommandName = commandName;
        Arguments = arguments;
    }

    public static CommandParseResult Empty() => new CommandParseResult(string.Empty, []);
}