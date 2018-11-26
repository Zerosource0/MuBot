﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class Playlist
    {
        public bool Collaborative { get; set; }

        /// <summary>
        /// The playlist description. Only returned for modified, verified playlists, otherwise null.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Known external URLs for this playlist.
        /// </summary>
        public Externals ExternalUrl { get; set; }

        /// <summary>
        /// Information about the followers of the playlist. 
        /// </summary>
        public Followers Followers { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the playlist.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The Spotify ID for the playlist. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The playlist image. Note that this field is only returned for modified, verified playlists, otherwise the 
        /// array is empty. If returned, the source URL for the image (url) is temporary and will expire in less than a day.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// The name of the playlist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user who owns the playlist
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// true if the playlist is not marked as secret. 
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// Information about the tracks of the playlist. 
        /// </summary>
        public Page<PlaylistTrack> Tracks { get; set; }

        /// <summary>
        /// The object type: "playlist"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        public string Uri { get; set; }

        [JsonConstructor]
        public Playlist(bool collaborative,
            string description,
            Externals externalUrl,
            Followers followers,
            string href,
            string id,
            Image[] images,
            string name,
            User owner,
            bool? @public,
            Page<PlaylistTrack> tracks,
            string type,
            string uri)
        {
            Collaborative = collaborative;
            Description = description;
            ExternalUrl = externalUrl;
            Followers = followers;
            Href = href;
            Id = id;
            Images = images.ToList();
            Name = name;
            Owner = owner;
            Public = @public.GetValueOrDefault();
            Tracks = tracks;
            Type = type;
            Uri = uri;
        }
    }
}
