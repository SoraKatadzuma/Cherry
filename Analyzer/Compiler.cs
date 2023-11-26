namespace Cherry.Analyzer; 

public static class Compiler {
    public static void CompileTarget(Target target) {
        // Check if target exists
        // Check if any of the files have been updated after last target build.
        // Load symbols from files that do not need to be recompiled?
        // Request parser to parse inputs that need to be recompiled.
        // Traverse tree and do LLVM code gen.
        // Anything else?
    }

    // Necessary?
    public static void LinkTarget(Target target) {
    }
}
