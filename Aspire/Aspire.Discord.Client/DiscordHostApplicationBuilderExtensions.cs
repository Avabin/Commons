using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aspire.Discord.Client;

public static class DiscordHostApplicationBuilderExtensions
{
    /// <summary>
    /// Add a Discord client to the service collection
    /// </summary>
    /// <param name="builder">the host builder</param>
    /// <param name="onToken">the function to create the client</param>
    /// <param name="resourceName">the resource name to get the token from</param>
    /// <typeparam name="T">the type of the client</typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when the token is not found</exception>
    public static IHostApplicationBuilder AddDiscordClient<T>(this IHostApplicationBuilder builder, Func<string, IConfiguration, T> onToken, string? resourceName = null) where T : class
    {
        builder.Services.AddSingleton<T>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            
            var token = config.GetToken(resourceName);
            if (token is not null) return onToken(token, config);
            
            var tokenKey = DiscordResourceConfig.GetTokenKey(resourceName);
            throw new InvalidOperationException($"Discord token not found for resource '{resourceName}' (key: '{tokenKey}')");

        });

        return builder;
    }
}