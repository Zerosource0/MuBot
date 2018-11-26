using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class Image
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        [JsonConstructor]
        public Image(string url, int? height, int? width)
        {
            Height = height.GetValueOrDefault();
            Width = width.GetValueOrDefault();
            Url = url;
        }
    }
}
