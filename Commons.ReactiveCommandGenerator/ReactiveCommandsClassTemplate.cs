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
    public static string RenderForClass(string @namespace, string className, IEnumerable<string> methods)
    {
        // render the reactive commands
        var sb = new StringBuilder();
        foreach (var method in methods)
        {
            // split the method signature
            var indexOfBracket = method.IndexOf('(');
            // split only until the method bracket
            var methodSignature = method[..indexOfBracket].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var indexOfEndBracket = method.IndexOf(')');
            var parameter = method[(indexOfBracket + 1)..indexOfEndBracket].Trim();
            var hasParameter = !string.IsNullOrWhiteSpace(parameter);

            // split method name to get the parameter and base name
            var methodName = methodSignature[^1];
            var methodNameParts =
                methodName.Split('(', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var baseMethodName = methodNameParts[0];

            // get the return type
            var returnType = methodSignature[^2];
            if (returnType == "void")
            {
                returnType = "";
            }

            // get the reactive command
            var reactiveCommand = ReactiveCommandTemplate.RenderReactiveCommand(baseMethodName, returnType, parameter);

            // append the reactive command
            sb.AppendLine(reactiveCommand);
        }

        // render the class
        var renderedClass = ReactiveCommandClassTemplate
            .Replace("{Namespace}", @namespace)
            .Replace("{ClassName}", className)
            .Replace("{Properties}", sb.ToString());

        return renderedClass;
    }

    private const string ReactiveCommandClassTemplate = """
                                                        namespace {Namespace};
                                                                                                                 
                                                        public partial class {ClassName}
                                                        {

                                                        {Properties}

                                                        }
                                                        """;
}