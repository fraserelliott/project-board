using System.Windows;

namespace ProjectManager.Views;

/// <summary>
/// Interaction logic for NewTaskDialog.xaml
/// </summary>
public partial class PromptForStringDialog : Window
{
    public PromptForStringDialog()
    {
        InitializeComponent();

        Loaded += (_, _) =>
        {
            ValueTextBox.Focus();
            ValueTextBox.SelectAll();
        };
    }
}
