namespace Application.Utilities; 

public static class PathHelper {
    public static bool Absolute { get; set; }

    public static string CalculatePath(string relativePath) {
        return !Absolute
            ? Path.GetRelativePath(Directory.GetCurrentDirectory(), relativePath)
            : Path.GetFullPath(relativePath);
    }
}
