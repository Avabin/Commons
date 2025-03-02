using Microsoft.Extensions.Configuration;

namespace Aspire.Discord.Client;

internal static class DiscordResourceConfig
{
    private const string TokenEnvKeyFormat = "DOTNET_{0}__Discord__Token";
    private const string DefaultResourceName = "Discord";

    public static string? GetToken(this IConfiguration config, string? resourceName = null)
    {
        var tokenKey = GetTokenKey(resourceName);
        
        return config[tokenKey];
    }
    
    public static string GetTokenKey(string? resourceName) => string.Format(TokenEnvKeyFormat, resourceName ?? DefaultResourceName);
}