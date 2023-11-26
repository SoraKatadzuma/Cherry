using Cherry.Analyzer;
using Cherry.Application.Configuration;
using Serilog;

namespace Cherry.Application.Utilities; 

public static class BuildSystem {
    private static readonly Queue<Action> _BUILD_QUEUE = new();

    public static void Build(CherryStem configuration) {
        Log.Logger.Information("Building project {0}", configuration.Project.Name);
        
        Compiler.CompileTarget(configuration.Targets[0]);
    }
}
