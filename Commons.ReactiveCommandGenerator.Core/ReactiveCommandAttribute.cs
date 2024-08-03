namespace Commons.ReactiveCommandGenerator.Core;

[AttributeUsage(AttributeTargets.Method)]
public class ReactiveCommandAttribute : Attribute
{
    public string? CanExecuteMethodName { get; }
    public ReactiveCommandAttribute(string? canExecuteMethodName = null)
    {
        CanExecuteMethodName = canExecuteMethodName;
    }
}
