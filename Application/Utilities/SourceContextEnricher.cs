using Serilog.Core;
using Serilog.Events;

namespace Cherry.Application.Utilities; 

public class SourceContextEnricher : ILogEventEnricher {
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
        if (!logEvent.Properties.TryGetValue("SourceContext", out var propertyValue))
            return;

        var scalarValue = propertyValue as ScalarValue;
        var stringValue = scalarValue?.Value as string;
        if (!stringValue?.StartsWith("Cherry.") ?? true)
            return;

        var firstElement = stringValue.Split(".").FirstOrDefault();
        if (string.IsNullOrEmpty(firstElement))
            return;
        
        logEvent.AddOrUpdateProperty(new LogEventProperty("SourceContext", new ScalarValue(firstElement)));
    }
}
