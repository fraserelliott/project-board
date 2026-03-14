using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ProjectBoard.ViewModels.Tasks;

namespace ProjectBoard.Windows;

/// <summary>
///     Interaction logic for TasksView.xaml
/// </summary>
public partial class TasksView : UserControl
{
    public TasksView()
    {
        InitializeComponent();
    }

    private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (WasClickOnInteractiveElement(e.OriginalSource as DependencyObject))
            return;

        if (sender is not DataGridRow row)
            return;

        if (row.Item is not TaskItemViewModel item)
            return;

        if (DataContext is not TasksViewModel vm)
            return;

        if (vm.ShowDetailsCommand.CanExecute(item.Id))
        {
            vm.ShowDetailsCommand.Execute(item.Id);
            e.Handled = true;
        }
    }

    private static bool WasClickOnInteractiveElement(DependencyObject? source)
    {
        while (source != null)
        {
            if (source is ButtonBase ||
                source is TextBox ||
                source is ComboBox ||
                source is CheckBox ||
                source is ToggleButton ||
                source is Hyperlink)
                return true;

            source = VisualTreeHelper.GetParent(source);
        }

        return false;
    }
}