using ProjectManager.Stores;

namespace ProjectManager.Services;

public interface IPromptService
{
    /// <summary>
    /// Shows a modal prompt. Returns true if user submitted successfully; false if cancelled.
    /// On submit, calls tryAccept(value). If Success=false, dialog stays open and shows Message.
    /// </summary>
    OperationResult? PromptForString(
        string title,
        string label,
        string submitText,
        Func<string, OperationResult> tryAccept,
        string? initialValue = null);
}
