namespace Cherry.Analyzer;

public record Target(
    IEnumerable<string>  Sources,
    IEnumerable<string>? Options,
    IEnumerable<string>? Defines,
    IEnumerable<string>? Includes,
    IEnumerable<string>? Libraries,
    IEnumerable<string>? SearchPaths);
