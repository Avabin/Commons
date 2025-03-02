using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Discord.Hosting;
/// <summary>
/// Provides extension methods for adding and configuring Discord resources in a distributed application.
/// </summary>
public static class DiscordResourceExtensions
{
    private const string TokenEnvKeyFormat = "DOTNET_{0}__Discord__Token";
    private const string ResourceNameKey = "DOTNET_Discord__ResourceName";
    private const string DefaultResourceName = "Discord";
    private const string UsingEnvKeyFormat = "DOTNET_{0}__Discord__Using";
    private const string InteractivityEnvKeyFormat = "DOTNET_{0}__Discord__Interactivity";
    private const string CommandsNextEnvKeyFormat = "DOTNET_{0}__Discord__CommandsNext";

    /// <summary>
    /// Adds a new Discord resource to the distributed application with the specified name.
    /// </summary>
    /// <param name="builder">The builder for the distributed application.</param>
    /// <param name="name">The name of the Discord resource.</param>
    /// <returns>A builder for configuring the newly added Discord resource.</returns>
    public static IResourceBuilder<DiscordResource> AddDiscord(this IDistributedApplicationBuilder builder, string name)
    {
        var resource = new DiscordResource(name);
        return builder.AddResource(resource);
    }
    
    /// <summary>
    /// Sets the token parameter for a Discord resource.
    /// </summary>
    /// <param name="builder">A builder for configuring a Discord resource.</param>
    /// <param name="token">The token parameter to set for the Discord resource.</param>
    /// <returns>A builder for further configuring the Discord resource.</returns>
    public static IResourceBuilder<DiscordResource> WithDiscordToken(this IResourceBuilder<DiscordResource> builder, IResourceBuilder<ParameterResource> token)
    {
        builder.Resource.DiscordToken = token;
        return builder;
    }
    
    /// <summary>
    /// Configures an executable resource to use a specified Discord resource by setting the appropriate environment variable.
    /// </summary>
    /// <param name="builder">A builder for configuring an executable resource.</param>
    /// <param name="discord">The Discord resource to use in the executable.</param>
    /// <returns>A builder for further configuring the executable resource.</returns>
    public static IResourceBuilder<ExecutableResource> WithDiscord(this IResourceBuilder<ExecutableResource> builder, IResourceBuilder<DiscordResource> discord)
    {
        ArgumentNullException.ThrowIfNull(discord.Resource.DiscordToken);
        return builder.WithEnvironment(context =>
        {
            var tokenEnvKey = string.Format(TokenEnvKeyFormat, discord.Resource.Name);
            context.EnvironmentVariables[tokenEnvKey] = discord.Resource.DiscordToken!.Resource.Value;
        });
    }
    
    /// <summary>
    /// Configures a container resource to use a specified Discord resource by setting the appropriate environment variable.
    /// </summary>
    /// <param name="builder">A builder for configuring a container resource.</param>
    /// <param name="discord">The Discord resource to use in the container.</param>
    /// <returns>A builder for further configuring the container resource.</returns>
    public static IResourceBuilder<ContainerResource> WithDiscord(this IResourceBuilder<ContainerResource> builder, IResourceBuilder<DiscordResource> discord)
    {
        ArgumentNullException.ThrowIfNull(discord.Resource.DiscordToken);
        return builder.WithEnvironment(context =>
        {
            var tokenEnvKey = string.Format(TokenEnvKeyFormat, discord.Resource.Name);
            context.EnvironmentVariables[tokenEnvKey] = discord.Resource.DiscordToken!.Resource.Value;
        });
    }
    
    /// <summary>
    /// Configures a project resource to use a specified Discord resource by setting the appropriate environment variable.
    /// </summary>
    /// <param name="builder">A builder for configuring a project resource.</param>
    /// <param name="discord">The Discord resource to use in the project.</param>
    /// <returns>A builder for further configuring the project resource.</returns>
    public static IResourceBuilder<ProjectResource> WithDiscord(this IResourceBuilder<ProjectResource> builder, IResourceBuilder<DiscordResource> discord)
    {
        ArgumentNullException.ThrowIfNull(discord.Resource.DiscordToken);
        return builder.WithEnvironment(context =>
        {
            var tokenEnvKey = string.Format(TokenEnvKeyFormat, discord.Resource.Name);
            context.EnvironmentVariables[tokenEnvKey] = discord.Resource.DiscordToken!.Resource.Value;
        });
    }
}