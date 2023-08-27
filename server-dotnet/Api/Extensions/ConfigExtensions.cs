using Api.Config;

namespace Api.Extensions;

public static class ConfigExtensions
{
    public static T GetConfig<T>(this ConfigurationManager manager) where T : class, IConfig
    {
        var className = typeof(T).Name;
        var configName = className.Replace("Config", string.Empty);

        var section = manager.GetSection(configName);
        var config = section.Get<T>();

        if (config is null)
        {
            throw new InvalidOperationException($"Configuration {configName} is invalid");
        }

        return config;
    }
}