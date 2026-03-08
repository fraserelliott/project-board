namespace ProjectManager.ViewModels.Tasks;

public sealed class ExistingTagOption : AddTagOption
{
    public TagViewModel Tag { get; }

    public ExistingTagOption(TagViewModel tag)
    {
        Tag = tag;
    }

    public override string DisplayText => Tag.Name;
}
