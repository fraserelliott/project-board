using System.IO;
using ProjectBoard.Models.Domain;
using ProjectBoard.Models.Persistence;

namespace ProjectBoard.Services;

public class FileProgramDataPersistenceService
{
    private readonly string _filePath;
    private readonly JsonProgramDataSerializer _serializer;

    public FileProgramDataPersistenceService(string filePath, JsonProgramDataSerializer serializer)
    {
        _filePath = filePath;
        _serializer = serializer;
    }

    public void Save(List<RecentProject> recentProjects)
    {
        try
        {
            var data = new ProgramData
            {
                RecentProjects = recentProjects.Select(p => p.ToData()).ToList()
            };
            var json = _serializer.Serialize(data);
            var tempPath = _filePath + ".tmp";
            File.WriteAllText(tempPath, json);
            File.Move(tempPath, _filePath, true);
        }
        catch (Exception ex)
        {
            // TODO: logging
            throw new InvalidOperationException("Failed to save recent projects.", ex);
        }
    }
}