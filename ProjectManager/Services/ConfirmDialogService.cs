using ProjectManager.ViewModels;
using ProjectManager.Views;
using System.Windows;

namespace ProjectManager.Services;

public sealed class ConfirmDialogService
{
    public bool PromptConfirm(string label, string submitText)
    {
        var vm = new ConfirmDialogViewModel(label, submitText);
        var dlg = new ConfirmDialog
        {
            DataContext = vm,
        };

        vm.RequestCloseSuccess += () =>
        {
            dlg.DialogResult = true;
            dlg.Close();
        };

        dlg.Owner = Application.Current?.MainWindow;

        return dlg.ShowDialog() == true;
    }
}
