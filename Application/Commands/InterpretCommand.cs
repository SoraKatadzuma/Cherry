using Spectre.Console.Cli;

namespace Cherry.Application.Commands; 

internal sealed class InterpretCommand : Command<InterpretCommand.Settings> {
    public sealed class Settings : LoggingSettings {
    }

    public override int Execute(CommandContext context, Settings settings) {
        throw new NotImplementedException();
    }
}
