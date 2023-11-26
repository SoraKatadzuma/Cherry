using System.Diagnostics;

namespace Cherry.Logging; 

public sealed class Logger {
    private LogLevel _cliLogLevel;
    private LogLevel _fileLogLevel;
    
    public Logger() {
        
    }
    
    public Task LogAsync(LogMessage message) {
        return Task.Run(() => LogSync(message));
    }
    
    public void LogSync(LogMessage message) {
        var timestamp = DateTime.Now.ToShortTimeString();
        var processId = Environment.ProcessId;
        
        // Log to console and to file.
    }
}
