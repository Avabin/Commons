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
        var methods = new List<string>
        {
            "void DoSomething()"
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
        var methods = new List<string>
        {
            "void DoSomethingWithParameter(int parameter)"
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
        var methods = new List<string>
        {
            "global::Some.Namespace.MyEntry DoSomethingAndReturn()"
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
        var methods = new List<string>
        {
            "async global::System.Threading.Tasks.Task DoSomethingAsync()"
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
        var methods = new List<string>
        {
            "async global::System.Threading.Tasks.Task DoSomethingWithParameterAsync(int parameter)"
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
        var methods = new List<string>
        {
            "int DoSomethingAndReturn()"
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
        var methods = new List<string>
        {
            "async global::System.Threading.Tasks.Task<int> DoSomethingAndReturnAsync()"
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
        var methods = new List<string>
        {
            "int DoSomethingWithParameterAndReturn(int parameter)"
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
        var methods = new List<string>
        {
            "async global::System.Threading.Tasks.Task<int> DoSomethingWithParameterAndReturnAsync(int parameter)"
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