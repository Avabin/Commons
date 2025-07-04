# Commons.ReactiveCommandGenerator.Core

Core attributes library for the ReactiveCommand source generator. This package contains the attributes needed to mark methods for reactive command generation.

## Installation

```bash
dotnet add package Commons.ReactiveCommandGenerator.Core
```

## Attributes

### ReactiveCommandAttribute

Mark methods with this attribute to generate corresponding ReactiveCommand properties.

```csharp
[ReactiveCommand]
private void MyMethod()
{
    // Your logic here
}
```

This will generate:
```csharp
public ReactiveCommand<Unit, Unit> MyMethodCommand => ReactiveCommand.Create(MyMethod);
```

### Usage with CanExecute

You can specify a CanExecute method by naming convention:

```csharp
[ReactiveCommand(canExecuteMethodName: nameof(CanDoSomethingExecute)]
private void DoSomething()
{
    // Command logic
}

private IObservable<bool> CanDoSomethingExecute()
{
    // Return true if command can execute
    return Observable.Return(true);
}
```

This generates:
```csharp
public ReactiveCommand<Unit, Unit> DoSomethingCommand => 
    ReactiveCommand.Create(DoSomething, CanDoSomethingExecute());
```

## Target Frameworks

- .NET Standard 2.1
- .NET 8.0

## License

MIT
