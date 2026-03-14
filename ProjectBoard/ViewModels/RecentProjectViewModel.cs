using CommunityToolkit.Mvvm.ComponentModel;
using ProjectBoard.Models.Domain;

namespace ProjectBoard.Models;

public sealed class RecentProjectViewModel : ObservableObject
{
    private readonly RecentProject _recentProject;

    public RecentProjectViewModel(RecentProject recentProject)
    {
        _recentProject = recentProject;
    }

    public string LastOpenedString
    {
        get
        {
            var diff = DateTime.Now - LastOpened;

            if (diff.TotalSeconds < 10)
                return "Last opened just now";

            if (diff.TotalMinutes < 1)
                return $"Last opened {(int)diff.TotalSeconds} seconds ago";

            if (diff.TotalHours < 1)
                return $"Last opened {(int)diff.TotalMinutes} minutes ago";

            if (LastOpened.Date == DateTime.Today)
                return $"Last opened today at {LastOpened:HH:mm}";

            if (LastOpened.Date == DateTime.Today.AddDays(-1))
                return $"Last opened yesterday at {LastOpened:HH:mm}";

            return $"Last opened {LastOpened:dd/MM/yyyy HH:mm}";
        }
    }

    public Guid Id => _recentProject.Id;
    public string Name => _recentProject.Name;
    public string FilePath => _recentProject.FilePath;
    public DateTime LastOpened => _recentProject.LastOpened;

    public void Refresh()
    {
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(FilePath));
        OnPropertyChanged(nameof(LastOpened));
        OnPropertyChanged(nameof(LastOpenedString));
    }
}