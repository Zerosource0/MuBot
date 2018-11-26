using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class Track
    {
        /// <summary>
        /// The Album on which the track appears. The Album object includes a link in href to full information about the Album. 
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// The artists who performed the track. Each artist object includes a link in href to more detailed information about the artist. 
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        ///  A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.  
        /// </summary>
        public List<string> AvailableMarkets { get; set; }

        /// <summary>
        /// The disc number (usually 1 unless the Album consists of more than one disc).  
        /// </summary>
        public int DiscNumber { get; set; }

        /// <summary>
        /// The track length in milliseconds.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Whether or not the track has explicit lyrics (true = yes it does; false = no it does not OR unknown).  
        /// </summary>
        public bool Explicit { get; set; }

        /// <summary>
        /// Known external IDs for the track.
        /// </summary>
        public Externals ExternalId { get; set; }

        /// <summary>
        /// Known external URLs for this track.
        /// </summary>
        public Externals ExternalUrl { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The Spotify ID for the track. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the track. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the track. The value will be between 0 and 100, with 100 being the most popular. 
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// A link to a 30 second preview (MP3 format) of the track.
        /// </summary>
        public string PreviewUrl { get; set; }

        /// <summary>
        /// The number of the track. If an Album has several discs, the track number is the number on the specified disc.
        /// </summary>
        public int TrackNumber { get; set; }

        /// <summary>
        /// The object type: "track".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the track.
        /// </summary>
        public string Uri { get; set; }

        [JsonConstructor]
        public Track(
            Album album,
            Artist[] artists,
            string[] available_markets,
            int disc_number,
            int duration_ms,
            bool @explicit,
            Externals external_ids,
            Externals external_urls,
            string href,
            string id,
            string name,
            int popularity,
            string preview_url,
            int track_number,
            string type,
            string uri)
        {
            Album = album;
            Artists = artists.ToList();
            AvailableMarkets = available_markets.ToList();
            DiscNumber = disc_number;
            Duration = duration_ms;
            Explicit = @explicit;
            ExternalId = external_ids;
            ExternalUrl = external_urls;
            Href = href;
            Id = id;
            Name = name;
            Popularity = popularity;
            PreviewUrl = preview_url;
            TrackNumber = track_number;
            Type = type;
            Uri = uri;
        }
    }
}
