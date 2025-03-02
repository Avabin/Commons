using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.ReactiveCommandGenerator;

public static class ReactiveCommandsClassTemplate
{
    /// <summary>
    /// Render the reactive commands for the given class
    /// </summary>
    /// <param name="namespace">Namespace of the class</param>
    /// <param name="className">Class name. MUST be fully qualified type name</param>
    /// <param name="methods">Methods to render as reactive commands. MUST have the method signature with fully qualified type names</param>
    /// <returns></returns>
    public static string RenderForClass(string @namespace, string className, IEnumerable<(string method, string canExecute)> methods, string accessModifier = "public")
    {
        // render the reactive commands
        var sb = new StringBuilder();
        foreach (var (method, canExecuteMethod) in methods)
        {
            // split the method signature
            var indexOfBracket = method.IndexOf('(');
            // split only until the method bracket
            var methodSignature = method[..indexOfBracket].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var indexOfEndBracket = method.IndexOf(')');
            var parameters = method[(indexOfBracket + 1)..indexOfEndBracket].Trim();
            var hasParameter = !string.IsNullOrWhiteSpace(parameters);
            var splitParameters = parameters.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            // split method name to get the parameter and base name
            var methodName = methodSignature[^1];

            // get the return type
            var returnType = methodSignature[^2];
            if (returnType == "void")
            {
                returnType = "";
            }

            // get the reactive command
            var reactiveCommand =
                ReactiveCommandTemplate.RenderReactiveCommand(methodName, returnType, splitParameters, canExecuteMethod);

            // append the reactive command
            sb.AppendLine(reactiveCommand);
        }

        // render the class
        var renderedClass = ReactiveCommandClassTemplate
            .Replace("{AccessModifier}", accessModifier)
            .Replace("{Namespace}", @namespace)
            .Replace("{ClassName}", className)
            .Replace("{Properties}", sb.ToString());

        return renderedClass;
    }

    private const string ReactiveCommandClassTemplate = """
                                                        namespace {Namespace};
                                                                                                                 
                                                        {AccessModifier} partial class {ClassName}
                                                        {

                                                        {Properties}

                                                        }
                                                        """;
}