# Commons.ReactiveCommandGenerator

A Roslyn source generator that automatically creates ReactiveUI ReactiveCommand properties from methods marked with the `ReactiveCommandAttribute`.

## Installation

```bash
dotnet add package Commons.ReactiveCommandGenerator
dotnet add package Commons.ReactiveCommandGenerator.Core
```

## Usage

1. Add the `ReactiveCommandAttribute` to methods you want to convert to reactive commands:

```csharp
using Commons.ReactiveCommandGenerator.Core;

public partial class MyViewModel
{
    [ReactiveCommand]
    private void DoSomething()
    {
        // Your logic here
    }

    [ReactiveCommand]
    private async Task DoSomethingAsync()
    {
        // Your async logic here
    }

    [ReactiveCommand]
    private void DoSomethingWithParameter(string parameter)
    {
        // Your logic with parameter here
    }
}
```

2. The generator will create reactive command properties:

```csharp
// Generated code
public partial class MyViewModel
{
    public ReactiveCommand<Unit, Unit> DoSomethingCommand => 
        ReactiveCommand.Create(DoSomething);

    public ReactiveCommand<Unit, Unit> DoSomethingAsyncCommand => 
        ReactiveCommand.CreateFromTask(DoSomethingAsync);

    public ReactiveCommand<string, Unit> DoSomethingWithParameterCommand => 
        ReactiveCommand.Create<string>(DoSomethingWithParameter);
}
```

## Features

- **Automatic command generation**: Converts methods to reactive commands
- **Async support**: Handles both synchronous and asynchronous methods
- **Parameter support**: Supports methods with parameters
- **CanExecute support**: Supports CanExecute predicates
- **Access modifiers**: Respects class access modifiers (public, internal, private, protected)
- **Multiple target frameworks**: Supports .NET 7.0, .NET 8.0, and .NET 9.0

## Requirements

- .NET 7.0 or higher
- ReactiveUI package in your project

## License

MIT