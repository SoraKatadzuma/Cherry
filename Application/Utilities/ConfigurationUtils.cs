using System.Text.RegularExpressions;
using Cherry.Application.Configuration;

namespace Cherry.Application.Utilities; 

internal static class ConfigurationUtils {
    private static readonly Regex _OPTION_REGEX = new Regex("^\\$\\{([A-Z_]+)\\}$");
    
    public static void UpdateConfigurationOptions(CherryStem          configuration,
                                                  IEnumerable<string> unSplitOptions) {
        var newOptions = unSplitOptions.Select(optStr => optStr.Split(":"))
                                       .ToDictionary(kvPair => kvPair[0],
                                                     kvPair => kvPair[1]);

        if (configuration.Options == null) {
            configuration.Options = newOptions.Select(
                kvPair => new Option {
                    Name  = kvPair.Key,
                    Value = kvPair.Value
                }
            ).ToList();
            return;
        }

        var altered = configuration.Options.Where(IsNewOption);
        foreach (var option in altered) {
            option.Value = newOptions[option.Name];
            newOptions.Remove(option.Name);
        }
        
        configuration.Options.AddRange(newOptions.Select(
            kvPair => new Option {
                Name  = kvPair.Key,
                Value = kvPair.Value
            }
        ));

        bool IsNewOption(Option option) =>
            newOptions.ContainsKey(option.Name);
    }

    public static void InterpolateConfigurationOptions(CherryStem configuration) {
        foreach (var target in configuration.Targets)
            InterpolateConfigurationOptions(target, configuration.Options);
    }

    private static void InterpolateConfigurationOptions(BuildTarget   target,
                                                        List<Option>? options) {
        if (options == null) return;
        target.Name = InterpolateConfigurationOptions(target.Name, options);
        InterpolateConfigurationOptions(target.Sources, options);
        InterpolateConfigurationOptions(target.Options, options); // These are different types of options.
        InterpolateConfigurationOptions(target.Defines, options);
        InterpolateConfigurationOptions(target.Includes, options);
        InterpolateConfigurationOptions(target.Libraries, options);
        InterpolateConfigurationOptions(target.SearchPaths, options);
    }

    private static void InterpolateConfigurationOptions(IList<string>? arguments,
                                                        List<Option>   options) {
        if (arguments == null) return;
        for (var index = 0; index < arguments.Count; index++)
            arguments[index] = InterpolateConfigurationOptions(arguments[index], options);
    }

    private static string InterpolateConfigurationOptions(string       argument,
                                                          List<Option> options) {
        var matches = _OPTION_REGEX.Matches(argument);
        if (matches.Count == 0) return argument;

        var  modified = argument;
        foreach (Match match in matches) {
            var toInsert = options.Find(opt => opt.Name == match.Value);
            if (toInsert == null) continue;
            
            modified = match.Result(toInsert.Value);
        }

        return modified;
    }
}
