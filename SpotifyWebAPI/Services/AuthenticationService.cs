using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyWebAPI.Extensions;

namespace SpotifyWebAPI.Services
{
    public class AuthenticationService
    {
        public static string ClientId { get; set; }

        public static string ClientSecret { get; set; }

        public static string RedirectUri { get; set; }

        public static async Task<AuthenticationToken> GetAccessToken(string code)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", RedirectUri},
                {"client_id", ClientId},
                {"client_secret", ClientSecret}
            };
            return await HttpHelper.Instance.Post("https://accounts.spotify.com/api/token", postData).Result.Deserialize<AuthenticationToken>();
        }
    }
}
