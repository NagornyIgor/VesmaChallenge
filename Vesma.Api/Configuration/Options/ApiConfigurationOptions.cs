namespace Vesma.Api.Configuration;

public class ApiConfigurationOptions : IOptionsConfig
{
    public static string OptionsSectionName => "ApiConfiguration";

    public required string[] AppServers { get; init; }
}
