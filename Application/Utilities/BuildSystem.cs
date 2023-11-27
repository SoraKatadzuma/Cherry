using Cherry.Application.Configuration;
using Serilog;

namespace Cherry.Application.Utilities; 

public static class BuildSystem {
    // TODO: support building multiple targets at once.
    public static void Build(CherryStem configuration) {
        Log.Logger.Information("Building project {0}", configuration.Project.Name);
        throw new NotImplementedException();
        // Compiler.CompileTarget(configuration.Targets[0], configuration.OutputPath);
    }
}
