using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class Album
    {
        // <summary>
        /// The type of the Album: one of "Album", "single", or "compilation". 
        /// </summary>
        public AlbumType AlbumType { get; set; }

        /// <summary>
        /// The artists of the Album. Each artist object includes a link in href to more detailed information about the artist.
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        /// The markets in which the Album is available: ISO 3166-1 alpha-2 country codes. Note that an Album is considered available in a market when at least 1 of its tracks is available in that market.
        /// </summary>
        public List<string> AvailableMarkets { get; set; }

        /// <summary>
        /// Known external IDs for the Album.
        /// </summary>
        public Externals ExternalId { get; set; }

        /// <summary>
        /// Known external URLs for this Album.
        /// </summary>
        public Externals ExternalUrl { get; set; }

        /// <summary>
        /// A list of the genres used to classify the Album. For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the Album.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the Album.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The cover art for the Album in various sizes, widest first.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// The name of the Album.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the Album. The value will be between 0 and 100, with 100 being the most popular. 
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// The popularity of the Album. The value will be between 0 and 100, with 100 being the most popular. 
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which release_date value is known: "year", "month", or "day".
        /// </summary>
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// The tracks of the Album.
        /// </summary>
        public Page<Track> Tracks { get; set; }

        /// <summary>
        /// The object type: "Album"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the Album. 
        /// </summary>
        public string Uri { get; set; }

        [JsonConstructor]
        public Album(
            string album_type, 
            Artist[] artists, 
            string[] available_markets, 
            Externals external_ids,
            Externals external_urls, 
            string[] genres, 
            string href, 
            string id, 
            Image[] images, 
            string name, 
            int popularity, 
            string release_date, 
            string release_date_precision, 
            Page<Track> tracks, 
            string type, 
            string uri)
        {
            AlbumType = Enum.Parse<AlbumType>(album_type);
            Artists = artists.ToList();
            AvailableMarkets = available_markets.ToList();
            ExternalId = external_ids;
            ExternalUrl = external_urls;
            Genres = genres.ToList();
            Href = href;
            Id = id;
            Images = images.ToList();
            Name = name;
            Popularity = popularity;
            ReleaseDatePrecision = release_date_precision;
            Tracks = tracks;
            Type = type;
            Uri = uri;

            if (DateTime.TryParse(release_date, out var dateTime))
                ReleaseDate = dateTime;

        }
    }
}
