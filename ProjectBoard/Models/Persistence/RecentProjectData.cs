namespace ProjectBoard.Models.Persistence;

public class RecentProjectData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
    public DateTime LastOpened { get; set; }
}