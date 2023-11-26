using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace Cherry.Application.Configuration; 

public sealed class CherryStem {
    [Required]
    [YamlMember(typeof(ProjectInfo), Alias = "project")]
    public ProjectInfo Project { get; set; } = new();

    [Required]
    [YamlMember(typeof(BuildTarget[]), Alias="targets")]
    public BuildTarget[] Targets { get; set; } = Array.Empty<BuildTarget>();
}
