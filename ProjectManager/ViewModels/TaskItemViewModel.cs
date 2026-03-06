using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManager.Models.Domain;
using ProjectManager.Stores;
using TaskStatus = ProjectManager.Models.Domain.TaskStatus;

namespace ProjectManager.ViewModels;

public sealed class TaskItemViewModel : ObservableObject
{
    private readonly ProjectSession _session;
    private readonly TaskItem _task;

    public TaskItemViewModel(ProjectSession session, TaskItem task)
    {
        _session = session;
        _task = task;
    }

    public Guid Id => _task.Id;
    public string Name => _task.Name;
    public string Description => _task.Description;
    public int Priority => _task.Priority;
    public TaskStatus Status => _task.Status;

    public bool IsBlocked => Status != TaskStatus.Completed && _session.IsTaskBlocked(Id);
    public bool IsStale => Status == TaskStatus.Completed && _session.IsTaskStale(Id);

    public string ButtonText =>
        Status == TaskStatus.NotStarted ? "Start" :
        Status == TaskStatus.Started ? "Complete" : "Reopen";

    private bool _editing;
    public bool IsEditing
    {
        get => _editing;
        set
        {
            if (_editing == value) return;
            _editing = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotEditing));
        }
    }
    public bool IsNotEditing => !IsEditing;

    public void Refresh()
    {
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Priority));
        OnPropertyChanged(nameof(Status));
        OnPropertyChanged(nameof(IsBlocked));
        OnPropertyChanged(nameof(IsStale));
        OnPropertyChanged(nameof(ButtonText));
    }
}