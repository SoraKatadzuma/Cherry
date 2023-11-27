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
    
    [YamlMember(typeof(Option[]), Alias="options")]
    public List<Option>? Options { get; set; }

    [YamlIgnore]
    public string ProjectPath { get; internal set; } = string.Empty;

    [YamlIgnore]
    public string OutputPath { get; internal set; } = string.Empty;
}
