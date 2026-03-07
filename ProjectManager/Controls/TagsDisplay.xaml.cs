using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace ProjectManager.Controls;

/// <summary>
/// Interaction logic for TagsDisplay.xaml
/// </summary>
public partial class TagsDisplay : UserControl
{
    public TagsDisplay()
    {
        InitializeComponent();
    }

    public IEnumerable Tags
    {
        get => (IEnumerable)GetValue(TagsProperty);
        set => SetValue(TagsProperty, value);
    }

    public static readonly DependencyProperty TagsProperty = DependencyProperty.Register
        (nameof(Tags), typeof(IEnumerable), typeof(TagsDisplay), new PropertyMetadata(null));
}
