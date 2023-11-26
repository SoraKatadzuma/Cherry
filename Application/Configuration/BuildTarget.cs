using System.ComponentModel.DataAnnotations;
using Cherry.Analyzer;
using YamlDotNet.Serialization;

namespace Cherry.Application.Configuration; 

public class BuildTarget {
    [Required]
    [YamlMember(typeof(string[]), Alias="sources")]
    public string[] Sources { get; set; } = Array.Empty<string>();

    [YamlMember(typeof(string[]), Alias="options")]
    public string[]? Options { get; set; }
    
    [YamlMember(typeof(string[]), Alias="defines")]
    public string[]? Defines { get; set; }
    
    [YamlMember(typeof(string[]), Alias="includes")]
    public string[]? Includes { get; set; }
    
    [YamlMember(typeof(string[]), Alias="libraries")]
    public string[]? Libraries { get; set; }
    
    [YamlMember(typeof(string[]), Alias="searchPaths")]
    public string[]? SearchPaths { get; set; }
    

    public static implicit operator BuildTarget(Target compilerTarget) {
        return new BuildTarget {
            Sources     = compilerTarget.Sources.ToArray(),
            Options     = compilerTarget.Options?.ToArray(),
            Defines     = compilerTarget.Defines?.ToArray(),
            Includes    = compilerTarget.Includes?.ToArray(),
            Libraries   = compilerTarget.Libraries?.ToArray(),
            SearchPaths = compilerTarget.SearchPaths?.ToArray()
        };
    }
    
    public static implicit operator Target(BuildTarget buildTarget) {
        return new Target(
            Sources:     buildTarget.Sources,
            Options:     buildTarget.Options,
            Defines:     buildTarget.Defines,
            Includes:    buildTarget.Includes, 
            Libraries:   buildTarget.Libraries,
            SearchPaths: buildTarget.SearchPaths);
    }
}
