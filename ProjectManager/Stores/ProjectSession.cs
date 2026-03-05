using ProjectManager.Models.Domain;

namespace ProjectManager.Stores;

public abstract record RefreshScope;
public record RefreshNone : RefreshScope;
public record RefreshProject : RefreshScope;
public record RefreshTask(Guid TaskId) : RefreshScope;

public record OperationResult(
    bool Success,
    RefreshScope Refresh,
    string? Message = null
    );

public sealed class ProjectSession
{
    public Project Project { get; }
    public bool IsDirty { get; private set; }

    public ProjectSession(Project project)
    {
        Project = project;
    }

    public void Save()
    {
        IsDirty = false;
        // TODO: saving and persistence service
    }

    private void MarkDirty()
    {
        IsDirty = true;
        Save(); // later: debounce/autosave
    }

    public bool IsTaskBlocked(Guid taskId)
    {
        return Project.IsBlocked(taskId);
    }

    public bool IsTaskStale(Guid taskId)
    {
        return Project.IsStale(taskId);
    }

    public OperationResult AdvanceStatus(Guid taskId)
    {
        if (!Project.HasTaskWithId(taskId))
            return new OperationResult(false, new RefreshProject(), "Task not found.");
        if (IsTaskBlocked(taskId))
            return new OperationResult(false, new RefreshNone(), "Task is blocked.");

        Project.AdvanceStatus(taskId);
        MarkDirty();
        return new OperationResult(true, new RefreshProject());
    }

    public OperationResult AddTask(string name)
    {
        if (Project.HasTaskWithName(name))
            return new OperationResult(false, new RefreshProject(), "A task with this name already exists.");

        var task = Project.AddTask(name);
        MarkDirty();
        return new OperationResult(true, new RefreshTask(task.Id));
    }

    public OperationResult RemoveTask(Guid taskId)
    {
        if (!Project.HasTaskWithId(taskId))
            return new OperationResult(false, new RefreshProject(), "Task not found.");

        Project.RemoveTask(taskId);
        MarkDirty();
        return new OperationResult(true, new RefreshProject());
    }

    public OperationResult RenameTask(Guid taskId, string newName)
    {
        newName = (newName ?? "").Trim();
        var task = Project.GetTask(taskId);

        if (task == null)
            return new OperationResult(false, new RefreshProject(), "Task not found.");
        if (string.Equals(task.Name, newName, StringComparison.OrdinalIgnoreCase))
            return new OperationResult(true, new RefreshNone());

        Project.RenameTask(taskId, newName);
        MarkDirty();
        return new OperationResult(true, new RefreshTask(taskId));
    }

    public TaskItem? GetTask(Guid id) => Project.GetTask(id);
}
