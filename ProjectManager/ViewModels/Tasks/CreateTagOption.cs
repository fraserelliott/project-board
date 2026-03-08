namespace ProjectManager.ViewModels.Tasks;

public sealed class CreateTagOption : AddTagOption
{
    public string Name { get; }

    public CreateTagOption(string name)
    {
        Name = name;
    }

    public override string DisplayText => $"Create tag \"{Name}\"";
}