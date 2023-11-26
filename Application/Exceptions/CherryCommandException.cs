namespace Cherry.Application.Exceptions; 

public class CherryCommandException : Exception {
    public int StatusCode { get; init; }
    
    public CherryCommandException(int statusCode, string? message)
        : base(message ?? CreateExceptionMessageFor(statusCode)) {
        StatusCode = statusCode;
    }
    
    private static string CreateExceptionMessageFor(int statusCode) {
        return string.Empty;
    }
}
