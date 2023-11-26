namespace Cherry.Logging; 

/// <summary>
/// Provides the properties needed to log a given message.
/// </summary>
/// <param name="Severity">
/// The severity of the log as decided by the caller.
/// </param>
/// <param name="Source">
/// The source of the log as described by the caller.
/// </param>
/// <param name="Message">
/// The message of the log.
/// </param>
public record LogMessage(
    LogLevel Severity,
    string   Source,
    string   Message);
