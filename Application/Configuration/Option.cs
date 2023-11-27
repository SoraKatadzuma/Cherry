using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace Cherry.Application.Configuration; 

public class Option {
    [Required]
    [YamlMember(typeof(string), Alias = "name")]
    public string Name { get; set; } = "NAME";

    [Required]
    [YamlMember(typeof(string), Alias="value")]
    public string Value { get; set; } = "VALUE";
    
    [YamlMember(typeof(string), Alias="desc")]
    public string? Description { get; set; }
}
