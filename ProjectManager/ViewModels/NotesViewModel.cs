using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManager.Stores;

namespace ProjectManager.ViewModels
{
    public sealed class NotesViewModel : ObservableObject
    {
        private readonly ProjectSession _session;

        public NotesViewModel(ProjectSession session)
        {
            _session = session;
        }
    }
}
