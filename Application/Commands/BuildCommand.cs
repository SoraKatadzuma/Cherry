using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Cherry.Application.Exceptions;
using Cherry.Application.Utilities;
using Serilog;
using Spectre.Console.Cli;

namespace Cherry.Application.Commands; 

internal class BuildCommand : LoggedCommand<BuildCommand.Settings> {
    public sealed class Settings : LoggingSettings {
        [CommandArgument(position: 0, template: "[PATH]")]
        [Description("Stores the path to the project that will be built.")]
        public string? ProjectPath { get; init; }
        
        [CommandOption(template: "-D|--define <VALUES>")]
        [Description("Stores the options to update within the configuration.")]
        public string[]? Options { get; init; }
    }

    // TODO: create internal exception which can return status code.
    public override int Execute([NotNull] CommandContext context,
                                [NotNull] Settings       settings) {
        SetupLogger<BuildCommand>(settings);
        
        PathHelper.Absolute = settings.Verbose;
        var projectPath       = settings.ProjectPath ?? ".";
        var projectConfigFile = string.Empty;
        try {
            var cherryStemConfiguration =
                ProjectUtils.TryLoadCherryStemFile(projectPath, out projectConfigFile);
            if (cherryStemConfiguration == null) {
                Log.Logger.Fatal("Loaded null CherryStem configuration!");
                return 1;
            }

            Log.Logger.Information("Loaded CherryStem configuration {0}", PathHelper.CalculatePath(projectConfigFile));
            if (settings.Options != null) {
                Log.Logger.Verbose("Updating configuration options...");
                ConfigurationUtils.UpdateConfigurationOptions(cherryStemConfiguration, settings.Options);
                
                Log.Logger.Verbose("Interpolating configuration options...");
                ConfigurationUtils.InterpolateConfigurationOptions(cherryStemConfiguration);
            }
            
            cherryStemConfiguration.ProjectPath = projectPath;
            cherryStemConfiguration.OutputPath  = ProjectUtils.GetOutputPath(cherryStemConfiguration);
            Log.Logger.Debug("Project output path {0}", PathHelper.CalculatePath(cherryStemConfiguration.OutputPath));

            BuildSystem.Build(cherryStemConfiguration);
        } catch (CherryCommandException cherryCommandException) {
            Log.Logger.Fatal(cherryCommandException, "Failed to execute build command");
            return cherryCommandException.StatusCode;
        }
        
        return 0;
    }
}
