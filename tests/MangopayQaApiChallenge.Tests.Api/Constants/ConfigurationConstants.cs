namespace MangopayQaApiChallenge.Tests.Api.Constants;

public static class ConfigurationConstants
{
    public static string AppSettingsFileName => "appSettings.json";

    public static string AppSettingsMissingError => "AppSettings configuration is missing or invalid.";

    public static string ClientIdRequiredError => "ClientId is required in configuration.";

    public static string ClientPasswordRequiredError => "ClientPassword is required in configuration.";
}
