using System.Diagnostics.CodeAnalysis;
using Application.Utilities;
using Serilog;
using Spectre.Console.Cli;

namespace Application.Commands; 

internal class BuildCommand : LoggedCommand<BuildCommand.Settings> {
    public sealed class Settings : LoggingSettings {
        // Stores the path to the project that will be built.
        [CommandArgument(position: 0, template: "[PATH]")]
        public string? ProjectPath { get; init; }
    }

    public override int Execute([NotNull] CommandContext context,
                                [NotNull] Settings       settings) {
        // Attempt to load configuration file from project path.
        PathHelper.Absolute = settings.Verbose;
        var projectPath = settings.ProjectPath ?? ".";
        if (!ProjectUtils.ProjectConfigExists(projectPath, out var projectConfigFile)) {
            SetupLogger<BuildCommand>(settings);
            Log.Logger.Verbose("Tried to verify project configuration.");
            Log.Logger.Fatal("Failed to locate project configuration {0}",
                             PathHelper.CalculatePath(projectConfigFile));
            return 1;
        }
        
        var loggingPath = Path.Join(settings.ProjectPath, ".cherry");
        Directory.CreateDirectory(loggingPath);
        SetupLogger<BuildCommand>(settings, loggingPath);
        
        return 0;
    }
    
    
}
