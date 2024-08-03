namespace Commons.ReactiveCommandGenerator.Core;

/// <summary>
/// Attribute to mark a method to reactive command generation
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ReactiveCommandAttribute : Attribute
{
    /// <summary>
    /// Optional method name to check if the command can be executed
    /// </summary>
    public string? CanExecuteMethodName { get; }
    /// <summary>
    /// Create a new instance of the attribute
    /// </summary>
    /// <param name="canExecuteMethodName">Optional method name to check if the command can be executed</param>
    public ReactiveCommandAttribute(string? canExecuteMethodName = null)
    {
        CanExecuteMethodName = canExecuteMethodName;
    }
}
