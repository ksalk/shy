
using Shy;
using Shy.BuiltinCommands;

public class CommandParserTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { "echo hello world", new string[] { "echo", "hello", "world" } },
            new object[] { "echo   hello   world", new string[] { "echo", "hello", "world" } }
        };

    [Theory]
    [InlineData("echo hello world", new string[] { "echo", "hello", "world" } )]
    [InlineData("echo   hello   world", new string[] { "echo", "hello", "world" })]
    [InlineData("echo \"hello world\"", new string[] { "echo", "hello world" } )]
    [InlineData("echo \"hello\" \"world\"", new string[] { "echo", "hello", "world" } )]
    [InlineData("echo \'hello world\'", new string[] { "echo", "hello world" } )]
    [InlineData("echo \'hello\' \'world\'", new string[] { "echo", "hello", "world" } )]
    [InlineData("echo hello\"world\"", new string[] { "echo", "helloworld" })]
    [InlineData("echo \"hello\"world", new string[] { "echo", "helloworld" })]
    [InlineData("echo hello\'world\'", new string[] { "echo", "helloworld" })]
    [InlineData("echo \'hello\'world", new string[] { "echo", "helloworld" })]
    [InlineData("echo \'hello\'\'world\'", new string[] { "echo", "helloworld" })]
    [InlineData("echo \"hello\"\"world\"", new string[] { "echo", "helloworld" })]
    [InlineData("echo hello\'\'world", new string[] { "echo", "helloworld" })]
    [InlineData("echo hello\"\"world", new string[] { "echo", "helloworld" })]
    [InlineData("echo \'hello    world\'", new string[] { "echo", "hello    world" })]
    [InlineData("echo \"hello    world\"", new string[] { "echo", "hello    world" })]
    [InlineData("echo \"shell\'s test\"", new string[] { "echo", "shell\'s test" })]
    [InlineData("echo \'he said \"hi\"\'", new string[] { "echo", "he said \"hi\"" })]
    [InlineData("echo \'\'", new string[] { "echo", "" })]
    [InlineData("echo \"\"", new string[] { "echo", "" })]
    [InlineData("", new string[0])]
    [InlineData("   ", new string[0])]
    [InlineData("echo \"hello\"\'world\'", new string[] { "echo", "helloworld" })]
    [InlineData("echo \'hello\'\"world\"", new string[] { "echo", "helloworld" })]
    [InlineData("echo\thello", new string[] { "echo", "hello" })]
    [InlineData("echo hello ", new string[] { "echo", "hello" })]
    [InlineData(" echo hello", new string[] { "echo", "hello" })]
    [InlineData("echo \'$HOME\'", new string[] { "echo", "$HOME" })]
    [InlineData("echo \"$HOME\"", new string[] { "echo", "$HOME" })]
    [InlineData("echo", new string[] { "echo" })]
    [InlineData("echo \'hello \" world\'", new string[] { "echo", "hello \" world" })]
    [InlineData("\'echo\' hello world", new string[] { "echo", "hello", "world" })]
    [InlineData("\"echo\" hello world", new string[] { "echo", "hello", "world" })]
    [InlineData("\'echo\'", new string[] { "echo" })]
    [InlineData("\"echo\"", new string[] { "echo" })]
    [InlineData("\'echo \' hello", new string[] { "echo ", "hello" })]
    [InlineData("\"echo \" hello", new string[] { "echo ", "hello" })]
    [InlineData("  \'echo\' hello", new string[] { "echo", "hello" })]
    [InlineData("  \"echo\"  hello", new string[] { "echo", "hello" })]
    [InlineData("\'echo\' \'hello\' \'world\'", new string[] { "echo", "hello", "world" })]
    [InlineData("\"echo\" \"hello\" \"world\"", new string[] { "echo", "hello", "world" })]
    [InlineData("\'ec\'\'ho\' hello", new string[] { "echo", "hello" })]
    [InlineData("\"ec\"\"ho\" hello", new string[] { "echo", "hello" })]
    [InlineData("\'echo hello\' world", new string[] { "echo hello", "world" } )]
    [InlineData("\'echo   hello\' world", new string[] { "echo   hello", "world" } )]
    [InlineData("\' echo  hello \' world", new string[] { " echo  hello ", "world" } )]
    [InlineData("\"echo hello\" world", new string[] { "echo hello", "world" } )]
    [InlineData("\"echo   hello\" world", new string[] { "echo   hello", "world" } )]
    [InlineData("\" echo  hello \" world", new string[] { " echo  hello ", "world" } )]
    public void Tokenize_ReturnsCorrectTokensCollection(string input, string[] expectedTokens)
    {
        // Act
        var result = CommandParser.Tokenize(input);

        // Assert
        Assert.Equal(expectedTokens, result);
    }
}