using Application.Configuration;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;
using Exception = System.Exception;

namespace Application.Utilities; 

public static class ProjectUtils {

    /// <summary>
    /// Checks if the project configuration for a given project path exists.
    /// </summary>
    /// <param name="pathToProject">
    /// The path to the project that we want to verify the configuration for.
    /// </param>
    /// <param name="projectConfigFile">
    /// The path to the configuration file if it is found.
    /// </param>
    /// <returns>
    /// True if the configuration file is found, false otherwise.
    /// </returns>
    public static bool ProjectConfigExists(in  string pathToProject,
                                           out string projectConfigFile) {
        // Verify initial path exists.
        projectConfigFile = string.Empty;
        if (string.IsNullOrEmpty(pathToProject) ||
            !Directory.Exists(pathToProject))
            return false;

        var projectConfigExists = false;
        var possibleConfigFiles = new[] {
            "CherryStem.yaml",
            "CherryStem.yml",
            "CherryStem",
        };
        
        foreach (var fileName in possibleConfigFiles) {
            projectConfigFile = Path.Join(pathToProject, fileName);
            if (!File.Exists(projectConfigFile))
                continue;

            projectConfigExists = true;
            break;
        }
        
        return projectConfigExists;
    }

    /// <summary>
    /// Attempts to load the cherry stem configuration file at the given path.
    /// </summary>
    /// <param name="pathToConfiguration">
    /// The path to the cherry stem configuration file.
    /// </param>
    /// <param name="configuration">
    /// The loaded configuration if it was able to be loaded, null otherwise.
    /// </param>
    /// <returns>
    /// True if the configuration was loaded, false otherwise.
    /// </returns>
    public static bool TryLoadCherryStemFile(in  string      pathToConfiguration,
                                             out CherryStem? configuration) {
        // If configuration doesn't exist, fail.
        configuration = null;
        if (!File.Exists(pathToConfiguration))
            return false;
        
        // Attempt to deserialize data from file.
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        var input = File.OpenText(pathToConfiguration);
        configuration = deserializer.Deserialize<CherryStem>(input);
        return true;
    }
}
