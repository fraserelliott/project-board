namespace ProjectBoard.Windows;

/// <summary>
///     Interaction logic for NewTaskDialog.xaml
/// </summary>
public partial class PromptForStringDialog : AppWindow
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