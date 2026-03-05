using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManager.Services;
using ProjectManager.Stores;

namespace ProjectManager.ViewModels
{
    public sealed class ShellViewModel : ObservableObject
    {
        public ProjectSession Session { get; }
        public TasksViewModel Tasks { get; }
        public NotesViewModel Notes { get; }

        private ObservableObject _currentViewModel;
        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                if (SetProperty(ref _currentViewModel, value))
                {
                    OnPropertyChanged(nameof(IsTasksActive));
                    OnPropertyChanged(nameof(IsNotesActive));
                }
            }
        }

        public IRelayCommand ShowTasksCommand { get; }
        public IRelayCommand ShowNotesCommand { get; }

        public bool IsTasksActive => CurrentViewModel == Tasks;
        public bool IsNotesActive => CurrentViewModel == Notes;

        private PromptService _promptService;

        public ShellViewModel(ProjectSession session)
        {
            Session = session;
            _promptService = new PromptService();
            Tasks = new TasksViewModel(session, _promptService);
            Notes = new NotesViewModel(session);

            ShowTasksCommand = new RelayCommand(() => CurrentViewModel = Tasks);
            ShowNotesCommand = new RelayCommand(() => CurrentViewModel = Notes);

            CurrentViewModel = Tasks;
        }
    }
}
