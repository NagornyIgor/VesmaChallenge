namespace Vesma.Api.Configuration;

public static class ConfigurationExtensions
{
    public static T GetConfigurationSection<T>(this IConfiguration config) where T : IOptionsConfig =>
        config.GetSection(T.OptionsSectionName).Get<T>()
            ?? throw new Exception($"Project configuration is not valid.  Can't find the {T.OptionsSectionName} config section.");
}
