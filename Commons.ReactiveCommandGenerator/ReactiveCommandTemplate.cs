using System;
using System.Linq;

namespace Commons.ReactiveCommandGenerator;

public static class ReactiveCommandTemplate
{
    /// <summary>
    /// Renders a ReactiveCommand property based on the method signature
    /// </summary>
    /// <param name="methodName">The name of the method</param>
    /// <param name="returnType">The return type of the method. MUST be a fully qualified type name</param>
    /// <param name="parameter">The parameter of the method. MUST be a fully qualified type name</param>
    /// <param name="canExecuteMethod">The name of the method that determines if the command can execute. Must returns a boolean</param>
    /// <returns></returns>
    public static string RenderReactiveCommand(string methodName, string returnType = "", string[]? parameters = null, string canExecuteMethod = "")
    {
        parameters ??= [];
        /*
         * Example
         *  Method:
         *     public void DoSomething()
         * ReactiveCommand:
         *    public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingCommand => global::ReactiveUI.ReactiveCommand.Create(DoSomething);
         *
         * Method:
         *    public void DoSomethingWithParameter(int parameter)
         * ReactiveCommand:
         *   public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterCommand => global::ReactiveUI.ReactiveCommand.Create<int>(DoSomethingWithParameter);
         *
         * Method:
         *   public async Task DoSomethingAsync()
         * ReactiveCommand:
         *  public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingAsyncCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask(DoSomethingAsync);
         *
         * Method:
         *  public async Task DoSomethingWithParameterAsync(int parameter)
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterAsyncCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingWithParameterAsync);
         *
         * Class : namespace Some.My.Namespace; public record Entry(string Name, string Value);
         * Method:
         * public Entry DoSomethingAndReturn()
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::Some.My.Namespace.Entry> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<global::Some.My.Namespace.Entry>(DoSomethingAndReturn);
         *
         * Method:
         * public async Task<Entry> DoSomethingAndReturnAsync()
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::Some.My.Namespace.Entry> DoSomethingAndReturnAsyncCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<global::Some.My.Namespace.Entry>(DoSomethingAndReturnAsync);
         *
         * Method:
         * public Entry DoSomethingWithParameterAndReturn(int parameter)
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<int, global::Some.My.Namespace.Entry> DoSomethingWithParameterAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<int, global::Some.My.Namespace.Entry>(DoSomethingWithParameterAndReturn);
         *
         * Method:
         * public async Task<Entry> DoSomethingWithParameterAndReturnAsync(int parameter)
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<int, global::Some.My.Namespace.Entry> DoSomethingWithParameterAndReturnAsyncCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int, global::Some.My.Namespace.Entry>(DoSomethingWithParameterAndReturnAsync);
         *
         * Method:
         * public IObservable<Entry> DoSomethingAndReturnObservable()
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.IObservable<global::Some.My.Namespace.Entry>> DoSomethingAndReturnObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable<global::Some.My.Namespace.Entry>(DoSomethingAndReturnObservable);
         *
         * Method:
         * public IObservable<Entry> DoSomethingWithParameterAndReturnObservable(int parameter)
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<int, global::System.IObservable<global::Some.My.Namespace.Entry>> DoSomethingWithParameterAndReturnObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable<int, global::Some.My.Namespace.Entry>(DoSomethingWithParameterAndReturnObservable);
         *
         * Method:
         * public IObservable<Unit> DoSomethingObservable()
         *
         * ReactiveCommand:
         * public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable(DoSomethingObservable);
         *
         */
        // we MUST use fully qualified type names
        // first, we need to check if the return type is a Task or IObservable
        var isTask = IsTask(returnType);
        var isObservable = IsObservable(returnType);

        // then, we need to check if the method has parameters
        var methodHasParameter = parameters.Any(x => x.Contains("CancellationToken")) ? parameters.Length > 1 : parameters.Length > 0;
        var methodParameterType = "";
        foreach (var parameter in parameters)
        {
            var parameterSplit = parameter.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var parameterType = methodHasParameter ? parameterSplit[^2] : string.Empty;
            
            if (parameterType.EndsWith("CancellationToken"))
            {
                break;
            }

            methodParameterType = parameterType;
        }


        // determine if the method has a return type
        // change above to switch statement
        var methodHasReturnType = returnType switch
        {
            "Task" or "Task<Unit>" or "IObservable<Unit>" or "global::System.Threading.Tasks.Task"
                or "global::System.Threading.Tasks.Task<global::System.Reactive.Unit>"
                or "global::System.IObservable<global::System.Reactive.Unit>" or "System.Threading.Tasks.Task"
                or "System.Threading.Tasks.Task<System.Reactive.Unit>"
                or "System.IObservable<System.Reactive.Unit>" => false,
            _ => true
        };

        // next step, we need to construct the ReactiveCommand.[CREATE_METHOD] as a property initializer
        // and CREATE_METHOD is either Create or CreateFromTask or CreateFromObservable
        // based on the return type and the method signature
        // ReactiveCommand.Create...<TParam, TRet> is generic, so we need to construct the generic type arguments\
        
        // if is task or observable, we need to transform the return type to the generic type argument
        var transformReturnType = "";
        if (isTask)
        {
            transformReturnType = returnType switch
            {
                "Task" or "global::System.Threading.Tasks.Task" => "global::System.Reactive.Unit",
                _ => returnType[(returnType.IndexOf('<') + 1)..^1]
            };
        }
        else if (isObservable)
        {
            transformReturnType = returnType[(returnType.IndexOf('<') +1)..^1];
        }
        else
        {
            // if not task or observable, we can use the return type as is
            transformReturnType = returnType;
        }
        if (transformReturnType == "") methodHasReturnType = false;
        // we can pass the method name as a parameter to the Create... method directly

        // but first, if method is async, we need to remove the *Async suffix if it exists
        var commandMethodName = methodName.EndsWith("Async") ? methodName[..^5] : methodName;
        
        var hasCanExecuteMethod = !string.IsNullOrWhiteSpace(canExecuteMethod);

        return (isTask, isObservable, methodHasParameter, methodHasReturnType, hasCanExecuteMethod) switch
        {
            // void DoSomething()
            (false, false, false, false, false) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create({methodName});",
            // void DoSomethingWithParameter(int parameter)
            (false, false, true, false, false) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create<{methodParameterType}>({methodName});",
            // async Task DoSomethingAsync()
            (true, false, false, false, false) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask({methodName});",
            // async Task DoSomethingWithParameterAsync(int parameter)
            (true, false, true, false, false) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask<{methodParameterType}>({methodName});",
            // Entry DoSomethingAndReturn()
            (false, false, false, true, false) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create<{transformReturnType}>({methodName});",
            // async Task<Entry> DoSomethingAndReturnAsync()
            (true, false, false, true, false) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask<{transformReturnType}>({methodName});",
            // Entry DoSomethingWithParameterAndReturn(int parameter)
            (false, false, true, true, false) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create<{methodParameterType}, {transformReturnType}>({methodName});",
            // async Task<Entry> DoSomethingWithParameterAndReturnAsync(int parameter)
            (true, false, true, true, false) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask<{methodParameterType}, {transformReturnType}>({methodName});",
            // IObservable<Entry> DoSomethingAndReturnObservable()
            (false, true, false, true, false) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.IObservable<{transformReturnType}>> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromObservable<{transformReturnType}>({methodName});",
            // IObservable<Entry> DoSomethingWithParameterAndReturnObservable(int parameter)
            (false, true, true, true, false) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, global::System.IObservable<{transformReturnType}>> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromObservable<{methodParameterType}, {transformReturnType}>({methodName});",
            // IObservable<Unit> DoSomethingObservable()
            (false, true, false, false, false) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromObservable({methodName});",
            // void DoSomething() with can execute
            (false, false, false, false, true) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create({methodName}, {canExecuteMethod}());",
            // void DoSomethingWithParameter(int parameter) with can execute
            (false, false, true, false, true) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create<{methodParameterType}>({methodName}, {canExecuteMethod}());",
            // async Task DoSomethingAsync() with can execute
            (true, false, false, false, true) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask({methodName}, {canExecuteMethod}());",
            // async Task DoSomethingWithParameterAsync(int parameter) with can execute
            (true, false, true, false, true) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask<{methodParameterType}>({methodName}, {canExecuteMethod}());",
            // Entry DoSomethingAndReturn() with can execute
            (false, false, false, true, true) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create<{transformReturnType}>({methodName}, {canExecuteMethod}());",
            // async Task<Entry> DoSomethingAndReturnAsync() with can execute
            (true, false, false, true, true) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask<{transformReturnType}>({methodName}, {canExecuteMethod}());",
            // Entry DoSomethingWithParameterAndReturn(int parameter) with can execute
            (false, false, true, true, true) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.Create<{methodParameterType}, {transformReturnType}>({methodName}, {canExecuteMethod}());",
            // async Task<Entry> DoSomethingWithParameterAndReturnAsync(int parameter) with can execute
            (true, false, true, true, true) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromTask<{methodParameterType}, {transformReturnType}>({methodName}, {canExecuteMethod}());",
            // IObservable<Entry> DoSomethingAndReturnObservable() with can execute
            (false, true, false, true, true) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.IObservable<{transformReturnType}>> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromObservable<{transformReturnType}>({methodName}, {canExecuteMethod}());",
            // IObservable<Entry> DoSomethingWithParameterAndReturnObservable(int parameter) with can execute
            (false, true, true, true, true) =>
                $"public global::ReactiveUI.ReactiveCommand<{methodParameterType}, {transformReturnType}> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromObservable<{methodParameterType}, {transformReturnType}>({methodName}, {canExecuteMethod}());",
            // IObservable<Unit> DoSomethingObservable() with can execute
            (false, true, false, false, true) =>
                $"public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> {commandMethodName}Command => global::ReactiveUI.ReactiveCommand.CreateFromObservable({methodName}, {canExecuteMethod}());",
            // default
            _ => string.Empty
        };
    }

    private static bool IsTask(string returnType)
    {
        var taskType = "System.Threading.Tasks.Task";
        var taskTypeName = "Task";
        var taskTypeFqn = "global::System.Threading.Tasks.Task";

        var isTaskType = returnType.StartsWith(taskType) || returnType.StartsWith(taskTypeName) ||
                         returnType.StartsWith(taskTypeFqn);


        return isTaskType;
    }

    private static bool IsObservable(string returnType)
    {
        var observableType = "System.IObservable";
        var observableTypeName = "IObservable";
        var observableTypeFqn = "global::System.IObservable";

        var isObservableType = returnType.StartsWith(observableType) || returnType.StartsWith(observableTypeName) ||
                               returnType.StartsWith(observableTypeFqn);

        return isObservableType;
    }
}