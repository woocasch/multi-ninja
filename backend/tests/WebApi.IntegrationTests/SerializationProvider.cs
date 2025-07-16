using System.Text.Json;

namespace MultiNinja.Backend.WebApi.IntegrationTests;

public static class SerializationProvider
{
    private static readonly JsonSerializerOptions SerializationOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, SerializationOptions);
    }

    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, SerializationOptions);
    }
}