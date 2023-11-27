namespace Cherry.Analyzer;

public record Target(
    string               Name,
    Target.OutputType    Type,
    IEnumerable<string>  Sources,
    IEnumerable<string>? Options,
    IEnumerable<string>? Defines,
    IEnumerable<string>? Includes,
    IEnumerable<string>? Libraries,
    IEnumerable<string>? SearchPaths) {

    public enum OutputType {
        Executable,
        StaticLib,
        DynamicLib,
        Invalid,
    }

    public bool TrimForRecompilation(string outputPath) {
        throw new NotImplementedException();
    }
}
