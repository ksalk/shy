using System;
using System.Linq;

namespace Shy.BuiltinCommands;

public class TypeCommand : BuiltinCommand
{
    public override string Name => "type";

    public override string Description => "display how command would be interpreted if used";

    public override BuiltinCommandResult Execute(string[] args)
    {
        if(args.Length == 0)
            return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);

        var command = args[0];

        if(BuiltinCommandsRegistry.Commands.Any(c => c.Name.Equals(command)))
        {
            Console.WriteLine($"{command} is a shell builtin");
            return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
        }

        var executable = ExecutableProvider.FindExecutableByName(command);
        if(executable != null)
        {
            Console.WriteLine($"{command} is a {executable}");
            return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
        }
        
        Console.WriteLine($"{command}: not found");
        return new BuiltinCommandResult(CommandExecutionResult.Success, PostCommandAction.None);
    }
}