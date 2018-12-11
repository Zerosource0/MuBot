using Models;
using SpotifyWebAPI.Services;

namespace MuBotWebApi.Extensions
{
    public static class TokenExtensions
    {
        public static SpotifyToken ToSpotifyToken(this AuthenticationToken token)
        {
            return new SpotifyToken()
            {
                AccessToken = token.AccessToken,
                TokenType =  token.TokenType,
                ExpiresOn = token.ExpiresOn,
                RefreshToken = token.RefreshToken
            };
        }

        public static AuthenticationToken ToAuthenticationToken(this SpotifyToken token)
        {
            return new AuthenticationToken()
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType,
                ExpiresOn = token.ExpiresOn,
                RefreshToken = token.RefreshToken
            };
        }
    }
}
