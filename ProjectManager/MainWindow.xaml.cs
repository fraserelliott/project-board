using ProjectManager.Models.Domain;
using ProjectManager.Stores;
using ProjectManager.ViewModels;
using System.Windows;
using System.Windows.Media;

namespace ProjectManager;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // temporary sample data to prove bindings work
        var project = new Project("My Project");
        var taskA = project.AddTask("Create context for authentication");
        var b = project.AddTask("Create login page");
        var c = project.AddTask("Create endpoint /api/users/login");
        project.AddDependency(taskA.Id, b.Id);
        project.AddDependency(b.Id, c.Id);

        var tag = project.AddTag("test", Colors.Red);
        taskA.AddTag(tag.Id);

        var tag2 = project.AddTag("a longer tag name", Colors.Green);
        taskA.AddTag(tag2.Id);

        var session = new ProjectSession(project);

        DataContext = new ShellViewModel(session);

        //var detailsWindow = new TaskDetailsWindow();
        //detailsWindow.Show();
    }
}