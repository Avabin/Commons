using Commons.ReactiveCommandGenerator;
using FluentAssertions;

namespace Avabin.Commons.ReactiveCommandGenerator.Tests;

[TestFixture]
public class Tests
{
    // void DoSomething()
    [Test]
    public void Method_VoidNoParamNoReturn()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("void DoSomething()", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingCommand => global::ReactiveUI.ReactiveCommand.Create(DoSomething);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // void DoSomethingWithParameter(int parameter)
    [Test]
    public void Method_VoidWithParamNoReturn()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("void DoSomethingWithParameter(int parameter)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterCommand => global::ReactiveUI.ReactiveCommand.Create<int>(DoSomethingWithParameter);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // global::Some.Namespace.MyEntry DoSomethingAndReturn()
    [Test]
    public void Method_ReturnEntryNoParam()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("global::Some.Namespace.MyEntry DoSomethingAndReturn()", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::Some.Namespace.MyEntry> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<global::Some.Namespace.MyEntry>(DoSomethingAndReturn);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // async Task DoSomethingAsync()
    [Test]
    public void Method_AsyncNoParamNoReturn()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task DoSomethingAsync()", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask(DoSomethingAsync);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // async Task DoSomethingWithParameterAsync(int parameter)
    [Test]
    public void Method_AsyncWithParamNoReturn()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task DoSomethingWithParameterAsync(int parameter)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingWithParameterAsync);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // int DoSomethingAndReturn()
    [Test]
    public void Method_ReturnIntNoParam()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("int DoSomethingAndReturn()", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, int> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<int>(DoSomethingAndReturn);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // async Task<int> DoSomethingAndReturnAsync()
    [Test]
    public void Method_ReturnIntAsyncNoParam()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task<int> DoSomethingAndReturnAsync()", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, int> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingAndReturnAsync);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}\r\nbut got: \r\n{result}\r\n");
    }
    
    // int DoSomethingWithParameterAndReturn(int parameter)
    [Test]
    public void Method_ReturnIntWithParam()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("int DoSomethingWithParameterAndReturn(int parameter)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<int, int>(DoSomethingWithParameterAndReturn);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task<int> DoSomethingWithParameterAndReturnAsync(int parameter)
    [Test]
    public void Method_ReturnIntAsyncWithParam()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task<int> DoSomethingWithParameterAndReturnAsync(int parameter)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int, int>(DoSomethingWithParameterAndReturnAsync);
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // void DoSomething() and CanDoSomethingExecute
    [Test]
    public void Method_VoidNoParamNoReturn_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("void DoSomething()", "CanDoSomethingExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingCommand => global::ReactiveUI.ReactiveCommand.Create(DoSomething, CanDoSomethingExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // void DoSomethingWithParameter(int parameter) and CanDoSomethingWithParameterExecute
    [Test]
    public void Method_VoidWithParamNoReturn_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("void DoSomethingWithParameter(int parameter)", "CanDoSomethingWithParameterExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterCommand => global::ReactiveUI.ReactiveCommand.Create<int>(DoSomethingWithParameter, CanDoSomethingWithParameterExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task DoSomethingAsync() and CanDoSomethingAsyncExecute
    [Test]
    public void Method_AsyncNoParamNoReturn_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task DoSomethingAsync()", "CanDoSomethingAsyncExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask(DoSomethingAsync, CanDoSomethingAsyncExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task DoSomethingWithParameterAsync(int parameter) and CanDoSomethingWithParameterAsyncExecute
    [Test]
    public void Method_AsyncWithParamNoReturn_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task DoSomethingWithParameterAsync(int parameter)", "CanDoSomethingWithParameterAsyncExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingWithParameterAsync, CanDoSomethingWithParameterAsyncExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // int DoSomethingAndReturn() and CanDoSomethingAndReturnExecute
    [Test]
    public void Method_ReturnIntNoParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("int DoSomethingAndReturn()", "CanDoSomethingAndReturnExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, int> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<int>(DoSomethingAndReturn, CanDoSomethingAndReturnExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task<int> DoSomethingAndReturnAsync() and CanDoSomethingAndReturnAsyncExecute
    [Test]
    public void Method_ReturnIntAsyncNoParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task<int> DoSomethingAndReturnAsync()", "CanDoSomethingAndReturnAsyncExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, int> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingAndReturnAsync, CanDoSomethingAndReturnAsyncExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // int DoSomethingWithParameterAndReturn(int parameter) and CanDoSomethingWithParameterAndReturnExecute
    [Test]
    public void Method_ReturnIntWithParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("int DoSomethingWithParameterAndReturn(int parameter)", "CanDoSomethingWithParameterAndReturnExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnCommand => global::ReactiveUI.ReactiveCommand.Create<int, int>(DoSomethingWithParameterAndReturn, CanDoSomethingWithParameterAndReturnExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task<int> DoSomethingWithParameterAndReturnAsync(int parameter) and CanDoSomethingWithParameterAndReturnAsyncExecute
    [Test]
    public void Method_ReturnIntAsyncWithParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task<int> DoSomethingWithParameterAndReturnAsync(int parameter)", "CanDoSomethingWithParameterAndReturnAsyncExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int, int>(DoSomethingWithParameterAndReturnAsync, CanDoSomethingWithParameterAndReturnAsyncExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // IObservable<Unit> DoSomethingAndReturnObservable() with CanExecute
    [Test]
    public void Method_ReturnObservableNoParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("global::System.IObservable<global::System.Reactive.Unit> DoSomethingAndReturnObservable()", "CanDoSomethingAndReturnObservableExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingAndReturnObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable(DoSomethingAndReturnObservable, CanDoSomethingAndReturnObservableExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // IObservable<int> DoSomethingWithParameterAndReturnObservable(int parameter) with CanExecute
    [Test]
    public void Method_ReturnObservableWithParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("global::System.IObservable<int> DoSomethingWithParameterAndReturnObservable(int parameter)", "CanDoSomethingWithParameterAndReturnObservableExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable<int, int>(DoSomethingWithParameterAndReturnObservable, CanDoSomethingWithParameterAndReturnObservableExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // IObservable<Unit> DoSomethingObservable() with CanExecute
    [Test]
    public void Method_ObservableNoParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("global::System.IObservable<global::System.Reactive.Unit> DoSomethingObservable()", "CanDoSomethingObservableExecute")
        };
        
        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);
        
        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable(DoSomethingObservable, CanDoSomethingObservableExecute());
                       
                       }
                       """;
        
        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // IObservable<int> DoSomethingWithParameterAndReturnObservable(int parameter) with CanExecute
    [Test]
    public void Method_ObservableWithParam_CanExecute()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("global::System.IObservable<int> DoSomethingWithParameterAndReturnObservable(int parameter)",
                "CanDoSomethingWithParameterAndReturnObservableExecute")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {

                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnObservableCommand => global::ReactiveUI.ReactiveCommand.CreateFromObservable<int, int>(DoSomethingWithParameterAndReturnObservable, CanDoSomethingWithParameterAndReturnObservableExecute());

                       }
                       """;

        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task DoSomethingAsync(CancellationToken token)
    [Test]
    public void Method_AsyncWithCancellation()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task DoSomethingAsync(global::System.Threading.CancellationToken token)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, global::System.Reactive.Unit> DoSomethingCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask(DoSomethingAsync);
                       
                       }
                       """;

        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task DoSomethingWithParameterAsync(int parameter, CancellationToken token)
    [Test]
    public void Method_AsyncWithParamAndCancellation()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task DoSomethingWithParameterAsync(int parameter, global::System.Threading.CancellationToken token)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, global::System.Reactive.Unit> DoSomethingWithParameterCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingWithParameterAsync);
                       
                       }
                       """;

        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task<int> DoSomethingAndReturnAsync(CancellationToken token)
    [Test]
    public void Method_AsyncWithCancellationAndReturn()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task<int> DoSomethingAndReturnAsync(global::System.Threading.CancellationToken token)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<global::System.Reactive.Unit, int> DoSomethingAndReturnCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int>(DoSomethingAndReturnAsync);
                       
                       }
                       """;

        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }
    
    // async Task<int> DoSomethingWithParameterAndReturnAsync(int parameter, CancellationToken token)
    [Test]
    public void Method_AsyncWithParamCancellationAndReturn()
    {
        // Arrange
        var @namespace = "Some.Module";
        var className = "SomeClass";
        var methods = new List<(string methodName, string canExecuteMethodName)>
        {
            ("async global::System.Threading.Tasks.Task<int> DoSomethingWithParameterAndReturnAsync(int parameter, global::System.Threading.CancellationToken token)", "")
        };

        // Act
        var result = ReactiveCommandsClassTemplate.RenderForClass(@namespace, className, methods);

        // Assert
        var expected = """
                       namespace Some.Module;
                       public partial class SomeClass
                       {
                       
                       public global::ReactiveUI.ReactiveCommand<int, int> DoSomethingWithParameterAndReturnCommand => global::ReactiveUI.ReactiveCommand.CreateFromTask<int, int>(DoSomethingWithParameterAndReturnAsync);
                       
                       }
                       """;

        // remove all whitespaces
        var resultReplaced = result.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        var expectedReplaced = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        resultReplaced.Should().Be(expectedReplaced, $"Expected: \r\n{expected}, \r\nbut got: \r\n{result}");
    }

}