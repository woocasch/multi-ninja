namespace MultiNinja.Backend.WebApi.Settings;

public class AuthorizationSettings
{
    public string AuthorizationUrl { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string MetadataAddress { get; set; } = string.Empty;
}