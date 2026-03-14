using ProjectBoard.Models.Persistence;

namespace ProjectBoard.Models.Domain;

public class RecentProject
{
    public RecentProject(Guid id, string name, string filePath, DateTime lastOpened)
    {
        Id = id;
        Name = name;
        FilePath = filePath;
        LastOpened = lastOpened;
    }

    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string FilePath { get; private set; }
    public DateTime LastOpened { get; private set; }

    public void Rename(string newName)
    {
        if (Name == newName) return;
        Name = newName;
    }

    public void UpdateFilePath(string filePath)
    {
        if (FilePath == filePath) return;
        FilePath = filePath;
    }

    public void UpdateLastOpened(DateTime lastOpened)
    {
        if (LastOpened == lastOpened) return;
        LastOpened = lastOpened;
    }

    public static RecentProject FromData(RecentProjectData data)
    {
        return new RecentProject(data.Id, data.Name, data.FilePath, data.LastOpened);
    }

    public RecentProjectData ToData()
    {
        return new RecentProjectData
        {
            Id = Id,
            Name = Name,
            FilePath = FilePath,
            LastOpened = LastOpened
        };
    }
}