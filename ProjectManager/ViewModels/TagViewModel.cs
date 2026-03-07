using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManager.Models.Domain;
using System.Windows.Media;

namespace ProjectManager.ViewModels;

public sealed class TagViewModel : ObservableObject
{
    private readonly Tag _tag;

    public string Name => _tag.Name;
    public Brush? Brush => _tag.Color.HasValue ? new SolidColorBrush(_tag.Color.Value) : null;

    public TagViewModel(Tag tag)
    {
        _tag = tag;
    }

    public void Refresh()
    {
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Brush));
    }
}
