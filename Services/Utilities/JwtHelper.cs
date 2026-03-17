using System.Text.Json;

namespace OfficeCore.Client.Services.Utilities;

/// <summary>
/// Helper class for JWT token operations without external dependencies.
/// Manually parses JWT tokens to check expiration.
/// </summary>
public static class JwtHelper
{
    /// <summary>
    /// Checks if a JWT token is expired.
    /// </summary>
    /// <param name="token">The JWT token string</param>
    /// <returns>True if the token is expired, false otherwise</returns>
    public static bool IsTokenExpired(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return true;

        try
        {
            var expiration = GetTokenExpiration(token);
            if (expiration == null)
                return true;

            // Add 5-second buffer for clock skew
            return DateTime.UtcNow > expiration.Value.AddSeconds(5);
        }
        catch
        {
            // If we can't parse the token, consider it expired
            return true;
        }
    }

    /// <summary>
    /// Gets the expiration time of a JWT token.
    /// </summary>
    /// <param name="token">The JWT token string</param>
    /// <returns>DateTime of expiration in UTC, or null if invalid</returns>
    public static DateTime? GetTokenExpiration(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
                return null;

            // Decode the payload (second part)
            var payload = parts[1];
            
            // Add padding if necessary
            var padding = 4 - (payload.Length % 4);
            if (padding != 4)
                payload += new string('=', padding);

            var decodedBytes = Convert.FromBase64String(payload);
            var jsonPayload = System.Text.Encoding.UTF8.GetString(decodedBytes);

            using var doc = JsonDocument.Parse(jsonPayload);
            if (doc.RootElement.TryGetProperty("exp", out var expProperty))
            {
                if (expProperty.TryGetInt64(out var expSeconds))
                {
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    return epoch.AddSeconds(expSeconds);
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the time remaining until token expiration.
    /// </summary>
    /// <param name="token">The JWT token string</param>
    /// <returns>TimeSpan of time remaining, or null if token is invalid</returns>
    public static TimeSpan? GetTimeUntilExpiration(string? token)
    {
        var expiration = GetTokenExpiration(token);
        if (expiration == null)
            return null;

        var timeRemaining = expiration.Value - DateTime.UtcNow;
        return timeRemaining > TimeSpan.Zero ? timeRemaining : TimeSpan.Zero;
    }
}
