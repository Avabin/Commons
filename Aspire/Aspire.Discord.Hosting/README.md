# Aspire.Discord.Hosting

.NET Aspire hosting integration for Discord applications. This package provides extensions to easily configure Discord resources in your .NET Aspire applications.

## Installation

```bash
dotnet add package Aspire.Discord.Hosting
```

## Usage

### Adding Discord Resources

Add Discord resources to your Aspire application:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Add a Discord resource
var discord = builder.AddDiscord("discord");

// Use the Discord resource in a project
var api = builder.AddProject<Projects.MyDiscordBot>("discord-bot")
    .WithDiscord(discord);

builder.Build().Run();
```

### Configuring Discord Token

The Discord token can be configured through user secrets or environment variables:

```bash
# Using user secrets
dotnet user-secrets set "DOTNET_Discord__Discord__Token" "your-discord-token"

# Or via environment variable
export DOTNET_Discord__Discord__Token="your-discord-token"
```

### Using with Containers

You can also use Discord resources with container applications:

```csharp
var discord = builder.AddDiscord("discord");

var container = builder.AddContainer("discord-container", "myregistry/discord-app")
    .WithDiscord(discord);
```

## API Reference

### DiscordResource

Represents a Discord application resource with token management.

### Extension Methods

- `AddDiscord(string name)` - Adds a Discord resource to the application
- `WithDiscord(IResourceBuilder<DiscordResource>)` - Configures a project to use a Discord resource

## Requirements

- .NET 9.0
- Aspire.Hosting

## License

MIT