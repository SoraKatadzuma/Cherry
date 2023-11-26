using System.ComponentModel.DataAnnotations;
using Semver;
using YamlDotNet.Serialization;

namespace Cherry.Application.Configuration; 

public class Identity {
    [Required]
    [YamlMember(typeof(string), Alias = "name")]
    public string Name { get; set; } = "Sample";

    [Required]
    [YamlMember(typeof(string), Alias = "gpid")]
    public string GroupId { get; set; } = "Group";
    
    [Required]
    [YamlMember(typeof(string), Alias="semv")]
    public SemVersion Version { get; set; } =
        SemVersion.ParsedFrom(0, 0, 1, "alpha", "dev");
}
