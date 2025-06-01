# Aspire.Discord.Client

.NET Aspire client integration for Discord applications. This package provides extensions to easily configure Discord clients in your .NET applications that are part of an Aspire distributed application.

## Installation

```bash
dotnet add package Aspire.Discord.Client
```

## Usage

### Adding Discord Client

Configure your Discord client in your application:

```csharp
using Aspire.Discord.Client;

var builder = Host.CreateApplicationBuilder(args);

// Add a Discord client
builder.AddDiscordClient<MyDiscordClient>((token, config) => 
{
    return new MyDiscordClient(token, config);
});

var app = builder.Build();
app.Run();
```

### With Resource Name

You can specify a resource name if you have multiple Discord resources:

```csharp
builder.AddDiscordClient<MyDiscordClient>(
    (token, config) => new MyDiscordClient(token, config),
    resourceName: "discord-bot"
);
```

### Token Configuration

The Discord token is automatically resolved from the configuration based on the resource name:

```bash
# Default resource name (Discord)
DOTNET_Discord__Discord__Token=your-token-here

# Custom resource name (discord-bot)
DOTNET_discord-bot__Discord__Token=your-token-here
```

### Example Implementation

```csharp
public class MyDiscordClient
{
    private readonly string _token;
    private readonly IConfiguration _config;

    public MyDiscordClient(string token, IConfiguration config)
    {
        _token = token;
        _config = config;
    }

    public async Task StartAsync()
    {
        // Initialize your Discord client with the token
        // e.g., using Discord.NET, DSharpPlus, etc.
    }
}
```

## API Reference

### Extension Methods

- `AddDiscordClient<T>(Func<string, IConfiguration, T> onToken, string? resourceName = null)` - Adds a Discord client to the service collection

### Configuration

The package uses the following environment variable pattern:
- Format: `DOTNET_{ResourceName}__Discord__Token`
- Default: `DOTNET_Discord__Discord__Token`

## Requirements

- .NET 9.0
- Microsoft.Extensions.Hosting.Abstractions

## License

MIT