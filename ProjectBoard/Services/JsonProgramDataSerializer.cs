using System.Text.Json;
using ProjectBoard.Models.Persistence;

namespace ProjectBoard.Services;

public class JsonProgramDataSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public string Serialize(ProgramData data)
    {
        return JsonSerializer.Serialize(data, _options);
    }

    public ProgramData Deserialize(string content)
    {
        return JsonSerializer.Deserialize<ProgramData>(content, _options) ??
               throw new InvalidOperationException("Failed to deserialize project.");
    }
}