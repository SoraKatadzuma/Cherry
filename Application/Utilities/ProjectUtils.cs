using Cherry.Application.Configuration;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Cherry.Application.Utilities; 

public static class ProjectUtils {
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

    public static CherryStem? TryLoadCherryStemFile(string pathToConfiguration) {
        CherryStem? configuration = default;
        if (!File.Exists(pathToConfiguration))
            return configuration;
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        var input = File.OpenText(pathToConfiguration);
        configuration = deserializer.Deserialize<CherryStem>(input);
        return configuration;
    }
}
