using Spectre.Console.Cli;

namespace Cherry.Application.Commands; 

public class LoggingSettings : CommandSettings {
    [CommandOption(template: "-v|--verbose")]
    public bool Verbose { get; init; } = false;
}
