using System.IdentityModel.Tokens.Jwt;

namespace OfficeCore.Client.Services.Utilities;

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
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return true;

            // Check if token expiration is in the past (add 5-second buffer for clock skew)
            return jwtToken.ValidTo < DateTime.UtcNow.AddSeconds(-5);
        }
        catch
        {
            // If we can't parse the token, consider it expired
            return true;
        }
    }

    /// <summary>
    /// Gets the time remaining until token expiration.
    /// </summary>
    /// <param name="token">The JWT token string</param>
    /// <returns>TimeSpan of time remaining, or null if token is invalid</returns>
    public static TimeSpan? GetTimeUntilExpiration(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var timeRemaining = jwtToken.ValidTo - DateTime.UtcNow;
            return timeRemaining > TimeSpan.Zero ? timeRemaining : TimeSpan.Zero;
        }
        catch
        {
            return null;
        }
    }
}
