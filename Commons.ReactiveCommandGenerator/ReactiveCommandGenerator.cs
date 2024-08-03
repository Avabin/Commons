using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Commons.ReactiveCommandGenerator;

[Generator]
public class ReactiveCommandGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // wait for debugger
        // if (!Debugger.IsAttached)
        // {
        //     Debugger.Launch();
        // }
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var syntaxTrees = context.Compilation.SyntaxTrees;
        var classesWithAttribute = new List<(string Namespace, string ClassName, List<(string, string)> Methods)>();

        foreach (var syntaxTree in syntaxTrees)
        {
            var root = syntaxTree.GetRoot();
            var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);

            IEnumerable<ClassDeclarationSyntax?> classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classDeclaration in classDeclarations)
            {
                if(classDeclaration is null) continue;
                var namespaceName = GetNamespace(classDeclaration);
                var className = classDeclaration.Identifier.Text;

                var methodsWithAttribute = (classDeclaration.Members.OfType<MethodDeclarationSyntax>()
                    .Select(method => new
                    {
                        method,
                        attributes =
                            method.AttributeLists.SelectMany(al => al.Attributes)
                    })
                    .Where(@t => @t.attributes.Any(attr =>
                        semanticModel.GetTypeInfo(attr).Type?.Name == "ReactiveCommandAttribute"))
                    .Select(@t => (method: FormatMethodDeclaration(@t.method, semanticModel), canExecute: GetCanExecuteMethodNameFromMethodDeclaration(@t.method, semanticModel))))
                    .ToList();

                if (methodsWithAttribute.Count != 0)
                {
                    classesWithAttribute.Add((namespaceName, className, methodsWithAttribute));
                }
            }
        }

        foreach (var (@namespace, className, methods) in classesWithAttribute)
        {
            var generatedCode = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
            var sourceText = SourceText.From(generatedCode, Encoding.UTF8);
            context.AddSource($"{className}.ReactiveCommands.g.cs", sourceText);
        }
    }

    private static string GetCanExecuteMethodNameFromMethodDeclaration(MethodDeclarationSyntax method, SemanticModel semanticModel)
    {
        // Get the attributes of the method
        var attributes = method.AttributeLists.SelectMany(al => al.Attributes);

        // Find the ReactiveCommandAttribute
        var reactiveCommandAttribute = attributes.FirstOrDefault(attr =>
            semanticModel.GetTypeInfo(attr).Type?.Name == "ReactiveCommandAttribute");

        // Get the CanExecuteMethodName argument
        var canExecuteArgument = reactiveCommandAttribute?.ArgumentList?.Arguments.FirstOrDefault();
        if (canExecuteArgument == null) return string.Empty;
        // Get the value of the CanExecuteMethodName argument
        var constantValue = semanticModel.GetConstantValue(canExecuteArgument.Expression);
        return constantValue is { HasValue: true, Value: string canExecuteMethodName } ? canExecuteMethodName : string.Empty;
    }
    
    private static string FormatMethodDeclaration(MethodDeclarationSyntax method, SemanticModel semanticModel)
    {
        // Get the fully qualified return type
        var returnTypeSymbol = semanticModel.GetSymbolInfo(method.ReturnType).Symbol as ITypeSymbol;
        var fullyQualifiedReturnType = returnTypeSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? method.ReturnType.ToString();

        // Get the method name
        var methodName = method.Identifier.Text;

        // Get the parameters
        var parameters = string.Join(", ", method.ParameterList.Parameters.Select(p =>
        {
            var parameterTypeSymbol = semanticModel.GetSymbolInfo(p.Type!).Symbol as ITypeSymbol;
            var fullyQualifiedParameterType = parameterTypeSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? p.Type?.ToString() ?? "";
            return $"{fullyQualifiedParameterType} {p.Identifier.Text}";
        }));

        // Format the method declaration
        return $"{fullyQualifiedReturnType} {methodName}({parameters})";
    }

    private static string GetNamespace(SyntaxNode? syntaxNode)
    {
        while (syntaxNode != null)
        {
            if (syntaxNode is NamespaceDeclarationSyntax namespaceDeclaration)
            {
                return namespaceDeclaration.Name.ToString();
            }

            if (syntaxNode is FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax)
            {
                return fileScopedNamespaceDeclarationSyntax.Name.ToString();
            }
            syntaxNode = syntaxNode.Parent ?? null;
        }
        return string.Empty;
    }
}