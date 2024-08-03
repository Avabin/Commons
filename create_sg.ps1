param (
    [string]$ProjectName
)

# Check if the project name is provided
if (-not $ProjectName) {
    Write-Host "Please provide a project name."
    exit 1
}

# Create the new source generator project
dotnet new console -n $ProjectName -f net8.0

# Navigate to the project directory
Set-Location -Path $ProjectName

# Add the necessary NuGet packages for a source generator
dotnet add package Microsoft.CodeAnalysis.CSharp --version 4.6.0
dotnet add package Microsoft.CodeAnalysis.Analyzers --version 3.3.3

# Create a basic source generator class
$sourceGeneratorCode = @"
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.Text;

[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Create the source code to inject into the user's compilation
        string source = @"
namespace HelloWorldGenerated
{
    public static class HelloWorld
    {
        public static void SayHello()
        {
            Console.WriteLine(\"Hello from generated code!\");
        }
    }
}";
        context.AddSource("helloWorldGenerator", SourceText.From(source, Encoding.UTF8));
    }
}
"@

# Save the source generator class to a file
$sourceGeneratorPath = Join-Path -Path $ProjectName -ChildPath "HelloWorldGenerator.cs"
# create the file
New-Item -Path $sourceGeneratorPath -ItemType File -Force | Out-Null
Set-Content -Path $sourceGeneratorPath -Value $sourceGeneratorCode

Write-Host "Source generator project '$ProjectName' created successfully."