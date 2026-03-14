using System.IO;
using ProjectBoard.Models.Domain;

namespace ProjectBoard.Services;

public abstract record UpdateResult;

public sealed record UpdatedRecentProject(Guid ProjectId) : UpdateResult;

public sealed record AddedRecentProject(RecentProject RecentProject) : UpdateResult;

public class RecentProjectsService
{
    private readonly FileProgramDataPersistenceService _persistenceService;
    private readonly List<RecentProject> _recentProjects = new();

    public RecentProjectsService()
    {
        _persistenceService =
            new FileProgramDataPersistenceService(GetProgramDataPath(), new JsonProgramDataSerializer());
    }

    public IReadOnlyList<RecentProject> RecentProjects => _recentProjects;
    public int MaxListLength { get; set; } = 10;

    public UpdateResult UpdateRecentProject(Guid projectId, string projectName, string filePath, DateTime lastOpened)
    {
        var project = _recentProjects.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            var newProject = new RecentProject(projectId, projectName, filePath, lastOpened);
            _recentProjects.Add(newProject);
            SortAndTrim();
            Save();
            return new AddedRecentProject(newProject);
        }

        project.Rename(projectName);
        project.UpdateFilePath(filePath);
        project.UpdateLastOpened(lastOpened);
        SortAndTrim();
        Save();
        return new UpdatedRecentProject(projectId);
    }

    private void SortAndTrim()
    {
        _recentProjects.Sort((a, b) => b.LastOpened.CompareTo(a.LastOpened));

        if (_recentProjects.Count > MaxListLength)
            _recentProjects.RemoveRange(MaxListLength, _recentProjects.Count - MaxListLength);
    }

    private void Save()
    {
        _persistenceService.Save(_recentProjects);
    }

    public void Load()
    {
        // TODO: implement loading recent projects
    }

    private static string GetProgramDataPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var folder = Path.Combine(appData, "FraserElliott", "ProjectBoard");
        Directory.CreateDirectory(folder);

        return Path.Combine(folder, "programdata.json");
    }
}