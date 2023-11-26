using Spectre.Console.Cli;

namespace Application.Commands; 

public class LoggingSettings : CommandSettings {
    [CommandOption(template: "-v|--verbose")]
    public bool Verbose { get; init; } = false;
}
