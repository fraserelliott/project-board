using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManager.Services;
using ProjectManager.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjectManager.ViewModels;

public sealed class TasksViewModel : ObservableObject
{
    private readonly ProjectSession _session;
    public IRelayCommand<Guid> AdvanceStatusCommand { get; }

    public string ProjectName => _session.Project.Name;

    public ObservableCollection<TaskItemViewModel> Tasks { get; }
    private readonly Dictionary<Guid, TaskItemViewModel> _tasksById;

    private TaskItemViewModel? _selectedTask;
    public TaskItemViewModel? SelectedTask
    {
        get => _selectedTask;
        set => SetProperty(ref _selectedTask, value);
    }

    public ICommand NewTaskCommand { get; }

    public TasksViewModel(ProjectSession session, IPromptService promptService)
    {
        _session = session;
        Tasks = new ObservableCollection<TaskItemViewModel>();
        _tasksById = new Dictionary<Guid, TaskItemViewModel>();

        foreach (var task in _session.Project.Tasks)
        {
            var vm = new TaskItemViewModel(_session, task);

            Tasks.Add(vm);
            _tasksById[vm.Id] = vm;
        }

        NewTaskCommand = new RelayCommand(() =>
        {
            var result = promptService.PromptForString("Add task", "Task name", "Add", name => session.AddTask(name));

            if (result is not { Success: true })
                return;

            if (result.Refresh is RefreshTask r)
            {
                var task = session.GetTask(r.TaskId);
                Tasks.Add(new TaskItemViewModel(session, task));
            }
        });

        AdvanceStatusCommand = new RelayCommand<Guid>(execute: AdvanceStatus, canExecute: id => !_session.IsTaskBlocked(id));
    }

    private void Notify(OperationResult result)
    {
        if (!result.Success)
            return;

        switch (result.Refresh)
        {
            case RefreshNone:
                break;

            case RefreshProject:
                RefreshAll();
                break;

            case RefreshTask(var taskId):
                GetTaskItemVM(taskId)?.Refresh();
                break;
        }
    }

    private void AdvanceStatus(Guid id)
    {
        var result = _session.AdvanceStatus(id);
        Notify(result);
    }

    public void RefreshAll()
    {
        OnPropertyChanged(nameof(ProjectName));
        AdvanceStatusCommand.NotifyCanExecuteChanged();
        foreach (var t in Tasks) t.Refresh();
    }

    public TaskItemViewModel? GetTaskItemVM(Guid taskId) =>
        _tasksById.TryGetValue(taskId, out var task)
                ? task
                : null;
}