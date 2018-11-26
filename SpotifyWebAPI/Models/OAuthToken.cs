using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class OAuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("TokenType")]
        public string TokenType { get; set; }

        [JsonProperty("ExpiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
