using ProjectManager.Models.Domain;

namespace ProjectManager.Stores
{
    public sealed class ProjectSession
    {
        public Project Project { get; }
        public bool IsDirty { get; private set; }

        public ProjectSession(Project project)
        {
            Project = project;
        }

        public bool IsTaskBlocked(Guid taskId)
        {
            return Project.IsBlocked(taskId);
        }

        public bool IsTaskStale(Guid taskId)
        {
            return Project.IsStale(taskId);
        }

        public void AdvanceStatus(Guid taskId)
        {
            Project.AdvanceStatus(taskId);
            MarkDirty();
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

        public TaskItem AddTask(string name)
        {
            var task = Project.AddTask(name);
            MarkDirty();
            return task;
        }

        public void RemoveTask(Guid taskId)
        {
            if (Project.RemoveTask(taskId))
                MarkDirty();
        }

        public void RenameTask(Guid taskId, string newName)
        {
            if (Project.RenameTask(taskId, newName))
                MarkDirty();
        }
    }
}
