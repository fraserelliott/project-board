using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectBoard.ViewModels.Tasks;

public sealed class TaskFilters : ObservableObject
{
    private bool _hideBlocked;
    private bool _hideCompleted;
    private bool _hideStale;

    public bool HideCompleted
    {
        get => _hideCompleted;
        set => SetProperty(ref _hideCompleted, value);
    }

    public bool HideStale
    {
        get => _hideStale;
        set => SetProperty(ref _hideStale, value);
    }

    public bool HideBlocked
    {
        get => _hideBlocked;
        set => SetProperty(ref _hideBlocked, value);
    }

    public void Reset()
    {
        HideCompleted = false;
        HideStale = false;
        HideBlocked = false;
    }
}