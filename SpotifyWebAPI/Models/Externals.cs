using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class Externals
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

    }
}
