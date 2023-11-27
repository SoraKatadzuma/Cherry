using System.Security.Cryptography;
using System.Text;
using Cherry.Application.Configuration;
using Serilog;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Cherry.Application.Utilities; 

internal static class ProjectUtils {
    public static CherryStem? TryLoadCherryStemFile(string pathToProject, out string projectConfigFile) {
        CherryStem? configuration = default;
        if (!ProjectConfigExists(pathToProject, out projectConfigFile))
            return configuration;

        var loggedPath = PathHelper.CalculatePath(projectConfigFile);
        Log.Logger.Information("Loading CherryStem configuration {0}", loggedPath);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        Log.Logger.Verbose("Opening configuration {0}", loggedPath);
        var input = File.OpenText(projectConfigFile);

        Log.Logger.Verbose("Deserializing configuration {0}", loggedPath);
        configuration = deserializer.Deserialize<CherryStem>(input);
        return configuration;
    }
    
    private static bool ProjectConfigExists(in  string pathToProject,
                                            out string projectConfigFile) {
        // Verify initial path exists.
        projectConfigFile = string.Empty;
        Log.Logger.Verbose("Checking project path...");
        if (string.IsNullOrEmpty(pathToProject) ||
            !Directory.Exists(pathToProject))
            return false;
        
        var projectConfigExists = false;
        var possibleConfigFiles = new[] {
            "CherryStem.yaml",
            "CherryStem.yml",
            "CherryStem",
        };
        
        Log.Logger.Verbose("Checking possible configuration files...");
        foreach (var fileName in possibleConfigFiles) {
            projectConfigFile = Path.Join(pathToProject, fileName);
            if (!File.Exists(projectConfigFile))
                continue;
            
            projectConfigExists = true;
            break;
        }
        
        return projectConfigExists;
    }

    public static string GetOutputPath(CherryStem configuration) {
        var stringBuilder = new StringBuilder();
        AppendCherryId(stringBuilder, configuration.Project);
        if (configuration.Options != null)
            AppendCombinedOptions(stringBuilder, configuration.Options);
        
        var unHashed   = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        var buildHash  = Convert.ToHexString(SHA1.Create().ComputeHash(unHashed));
        var outputPath = Path.Join(configuration.ProjectPath, ".cherry");
        return Path.Join(outputPath, buildHash);
    }

    private static void AppendCherryId(StringBuilder builder, Identity project) {
        builder.Append($"{project.GroupId}/");
        builder.Append($"{project.Name}@");
        builder.Append($"{project.Version}#");
    }

    private static void AppendCombinedOptions(StringBuilder builder, List<Option> options) {
        foreach (var option in options) builder.Append($"{option.Name}:{option.Value}");
    }
}
