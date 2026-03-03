using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManager.Stores;
using System.Collections.ObjectModel;

namespace ProjectManager.ViewModels;

public sealed class TasksViewModel : ObservableObject
{
    private readonly ProjectSession _session;
    public IRelayCommand<Guid> AdvanceStatusCommand { get; }

    public TasksViewModel(ProjectSession session)
    {
        _session = session;

        Tasks = new ObservableCollection<TaskItemViewModel>(
            _session.Project.Tasks.Select(t => new TaskItemViewModel(_session, t))
        );

        AdvanceStatusCommand = new RelayCommand<Guid>(execute: AdvanceStatus, canExecute: id => !_session.IsTaskBlocked(id));
    }

    private void AdvanceStatus(Guid id)
    {
        _session.AdvanceStatus(id);
        RefreshAll();
    }

    public string ProjectName => _session.Project.Name;

    public ObservableCollection<TaskItemViewModel> Tasks { get; }

    private TaskItemViewModel? _selectedTask;
    public TaskItemViewModel? SelectedTask
    {
        get => _selectedTask;
        set => SetProperty(ref _selectedTask, value);
    }

    public void RefreshAll()
    {
        OnPropertyChanged(nameof(ProjectName));
        AdvanceStatusCommand.NotifyCanExecuteChanged();
        foreach (var t in Tasks) t.Refresh();
    }
}