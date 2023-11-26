using YamlDotNet.Serialization;

namespace Cherry.Application.Configuration; 

public sealed class ProjectInfo : Identity {
    [YamlMember(typeof(string[]), Alias="authors")]
    public string[]? Authors { get; set; }
    
    [YamlMember(typeof(string), Alias="desc")]
    public string? Description { get; set; }
    
    [YamlMember(typeof(string), Alias="home")]
    public string? HomeWebsite { get; set; }
    
    [YamlMember(typeof(string), Alias="issues")]
    public string? IssuesWebsite { get; set; }
    
    [YamlMember(typeof(string), Alias="license")]
    public string? License { get; set; }
}
