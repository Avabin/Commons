using Aspire.Hosting.ApplicationModel;

namespace Aspire.Discord.Hosting;

/// <summary>
/// Represents a resource for hosting a Discord application.
/// </summary>
public class DiscordResource : Resource, IResourceWithEnvironment
{
    /// <summary>
    /// Gets or sets the builder for the Discord token parameter resource.
    /// </summary>
    public IResourceBuilder<ParameterResource>? DiscordToken { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DiscordResource"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    public DiscordResource(string name) : base(name)
    {
    }
}