using System.Diagnostics.CodeAnalysis;
using Cherry.Application.Exceptions;
using Cherry.Application.Utilities;
using Serilog;
using Spectre.Console.Cli;

namespace Cherry.Application.Commands; 

internal class BuildCommand : LoggedCommand<BuildCommand.Settings> {
    public sealed class Settings : LoggingSettings {
        [CommandArgument(position: 0, template: "[PATH]")]
        public string? ProjectPath { get; init; }
    }

    // TODO: create internal exception which can return status code.
    public override int Execute([NotNull] CommandContext context,
                                [NotNull] Settings       settings) {
        PathHelper.Absolute = settings.Verbose;
        var projectPath = settings.ProjectPath ?? ".";
        if (!ProjectUtils.ProjectConfigExists(projectPath, out var projectConfigFile)) {
            SetupLogger<BuildCommand>(settings);
            Log.Logger.Verbose("Tried to verify CherryStem configuration.");
            Log.Logger.Fatal("Failed to locate CherryStem configuration {0}",
                             PathHelper.CalculatePath(projectConfigFile));
            return 1;
        }
        
        Log.Logger.Information("Located CherryStem configuration {0}",
                               PathHelper.CalculatePath(projectConfigFile));
        
        var loggingPath = Path.Join(settings.ProjectPath, ".cherry");
        Directory.CreateDirectory(loggingPath);
        SetupLogger<BuildCommand>(settings, loggingPath);
        
        Log.Logger.Information("Loading CherryStem configuration {0}",
                               PathHelper.CalculatePath(projectConfigFile));
        try {
            var cherryStemConfiguration = ProjectUtils.TryLoadCherryStemFile(projectConfigFile);
            if (cherryStemConfiguration == null) {
                Log.Logger.Fatal("Loaded null CherryStem configuration!");
                return 1;
            }
            
            Log.Logger.Information("Loaded CherryStem configuration {0}",
                                   PathHelper.CalculatePath(projectConfigFile));

            BuildSystem.Build(cherryStemConfiguration);
        } catch (CherryCommandException cherryCommandException) {
            Log.Logger.Fatal(cherryCommandException,
                             "Failed to load CherryStem configuration {0}",
                             PathHelper.CalculatePath(projectConfigFile));
            return cherryCommandException.StatusCode;
        }
        
        return 0;
    }
    
    
}
