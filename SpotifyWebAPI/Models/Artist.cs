using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyWebAPI.Services;

namespace SpotifyWebAPI.Models
{
    public class Artist
    {
        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        public Externals ExternalUrl { get; set; }

        /// <summary>
        /// A list of the genres the artist is associated with. For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The Spotify ID for the artist. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// The name of the artist 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular.
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// The object type: "artist"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        public string Uri { get; set; }

        [JsonConstructor]
        public Artist(
            Externals external_urls, 
            string[] genres,
            string href,
            string id,
            Image[] images,
            string name,
            int popularity,
            string type,
            string uri)
        {
            ExternalUrl = external_urls;
            Genres = genres.ToList();
            Href = href;
            Id = id;
            Images = images.ToList();
            Name = name;
            Popularity = popularity;
            Type = type;
            Uri = uri;
        }


        public async Task<List<Track>> GetTopTracks(string countryCode = "DK")
        {
            return await ArtistService.Instance.GetTopTracks(Id, countryCode);
        }
    }
}
