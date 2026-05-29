using System;
using System.Collections.Generic;

namespace Shy;

public static class CommandParser
{
    public static CommandParseResult Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return CommandParseResult.Empty();
        }

        var commandTokens = Tokenize(input);
        if(commandTokens.Length == 0)
        {
            return CommandParseResult.Empty();
        }

        return new CommandParseResult(commandTokens[0], commandTokens[1..]);
    }

    public static string[] Tokenize(string input)
    {
        const char SingleQuote = '\'';
        const char DoubleQuote = '"';
        
        TokenType currentTokenType = TokenType.None;
        string currentToken = string.Empty;
        List<string> tokens = new List<string>();

        foreach (var character in input)
        {
            switch (currentTokenType)
            {
                case TokenType.None:
                    if (character == SingleQuote)
                        currentTokenType = TokenType.SingleQuotes;
                    else if (character == DoubleQuote)
                        currentTokenType = TokenType.DoubleQuotes;
                    else if (char.IsWhiteSpace(character))
                        continue;
                    else
                    {
                        currentTokenType = TokenType.String;
                        currentToken += character;
                    }
                    break;

                case TokenType.String:
                    if (character == SingleQuote)
                        currentTokenType = TokenType.SingleQuotes;
                    else if (character == DoubleQuote)
                        currentTokenType = TokenType.DoubleQuotes;
                    else if (char.IsWhiteSpace(character))
                    {
                        tokens.Add(currentToken);
                        currentToken = string.Empty;
                        currentTokenType = TokenType.None;
                    }
                    else
                    {
                        currentToken += character;
                    }
                    break;

                case TokenType.SingleQuotes:
                    if (character == SingleQuote)
                    {
                        currentTokenType = TokenType.String;
                    }
                    else if (character == DoubleQuote)
                    {
                        currentToken += character;
                    }
                    else if (char.IsWhiteSpace(character))
                    {
                        currentToken += character;
                    }
                    else
                    {
                        currentToken += character;
                    }
                    break;

                case TokenType.DoubleQuotes:
                    if (character == SingleQuote)
                    {
                        currentToken += character;
                    }
                    else if (character == DoubleQuote)
                    {
                        currentTokenType = TokenType.String;
                    }
                    else if (char.IsWhiteSpace(character))
                    {
                        currentToken += character;
                    }
                    else
                    {
                        currentToken += character;
                    }
                    break;
            }
        }

        if (currentTokenType == TokenType.String)
        {
            tokens.Add(currentToken);
        }

        return tokens.ToArray();
    }
}

public enum TokenType
{
    None = 0,
    String = 1,
    SingleQuotes = 2,
    DoubleQuotes = 3
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