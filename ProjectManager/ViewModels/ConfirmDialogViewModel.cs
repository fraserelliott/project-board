using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ProjectManager.ViewModels;

public sealed class ConfirmDialogViewModel : ObservableObject
{
    public string Label { get; }
    public string SubmitText { get; }
    public ICommand SubmitCommand { get; }
    public event Action? RequestCloseSuccess;

    public ConfirmDialogViewModel(string label, string submitText)
    {
        Label = label;
        SubmitText = submitText;
        SubmitCommand = new RelayCommand(Submit);
    }

    private void Submit()
    {
        RequestCloseSuccess?.Invoke();
    }
}
