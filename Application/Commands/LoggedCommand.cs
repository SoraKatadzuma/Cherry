using Cherry.Application.Utilities;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Spectre;
using Spectre.Console.Cli;

namespace Cherry.Application.Commands; 

internal abstract class LoggedCommand<TSettings> : Command<TSettings>
    where TSettings : LoggingSettings {

    private const string _TEMPLATE =
"""
{Timestamp:HH:mm:ss} {Level} [{SourceContext:l}]: {Message}{NewLine}{Exception}
""";
    
    protected static void SetupLogger<T>(LoggingSettings settings,
                                         string?         loggingPath = null) {
        var logLevel = settings.Verbose
            ? LogEventLevel.Verbose
            : LogEventLevel.Information;
        
        var config = new LoggerConfiguration();
        config.MinimumLevel.Verbose();
        config.Enrich.With<SourceContextEnricher>();
        config.WriteTo.Spectre(outputTemplate: _TEMPLATE, restrictedToMinimumLevel: logLevel);
        if (loggingPath != null) {
            // Adjust logging path to write to a specific file.
            var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            loggingPath = Path.Join(loggingPath, $"{datetime}.log");
            config.WriteTo.File(outputTemplate: _TEMPLATE, restrictedToMinimumLevel: logLevel, path: loggingPath);
        }

        Log.Logger = config.CreateLogger().ForContext<T>();
        Log.Logger.Verbose("Logging enabled.");
    }
}
