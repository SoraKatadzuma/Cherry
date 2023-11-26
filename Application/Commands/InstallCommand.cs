using Spectre.Console.Cli;

namespace Application.Commands; 

internal sealed class InstallCommand : Command<InstallCommand.Settings> {
    public sealed class Settings : LoggingSettings {
    }

    public override int Execute(CommandContext context, Settings settings) {
        throw new NotImplementedException();
    }
}
