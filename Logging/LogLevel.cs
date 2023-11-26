namespace Cherry.Logging; 

/// <summary>
/// Defines the logging levels that the logger will use to filter out messages.
/// </summary>
public enum LogLevel {
    /// <summary>
    /// Defines that there is no specific level of logging.
    /// </summary>
    None,
    
    /// <summary>
    /// Lowest form of logging, traces steps of the application.
    /// </summary>
    Trace,
    
    /// <summary>
    /// Defines the level of logging which tracks debugging messages of the
    /// application.
    /// </summary>
    Debug,
    
    /// <summary>
    /// Defines the level of logging which tracks informative messages of the
    /// application.
    /// </summary>
    Info,
    
    /// <summary>
    /// Defines the level of logging which tracks warning messages of the
    /// application.
    /// </summary>
    Warn,
    
    /// <summary>
    /// Defines the level of logging which tracks error messages of the
    /// application.
    /// </summary>
    Error,
    
    /// <summary>
    /// Defines the level of logging which tracks critical messages of the
    /// application. Forcibly stops the application if one is encountered.
    /// </summary>
    Critical,
}
