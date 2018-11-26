using System;
using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    public class PlaylistTrack
    {
        public DateTime AddedAt { get; set; }

        public User AddedBy { get; set; }

        public Track Track { get; set; }

        [JsonConstructor]
        public PlaylistTrack(string added_at, User added_by, Track track)
        {
            AddedAt = DateTime.TryParse(added_at, out DateTime addedAt) ? addedAt : DateTime.Now;
            AddedBy = added_by;
            Track = track;
        }
    }
}
