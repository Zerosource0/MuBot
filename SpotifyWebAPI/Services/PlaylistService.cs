using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SpotifyWebAPI.Extensions;
using SpotifyWebAPI.Models;
using SpotifyWebAPI.Services.BaseClasses;

namespace SpotifyWebAPI.Services
{
    public class PlaylistService : Singleton<PlaylistService>
    {
        public async Task<Playlist> GetPlaylist(string playlistId)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/playlists/" + playlistId).Result.Deserialize<Playlist>();
        }

        public async Task<Page<Track>> GetPlaylistTracks(string playlistId)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/playlists/" + playlistId + "/tracks").Result.Deserialize<Page<Track>>();
        }

        public async Task<Page<Playlist>> GetUsersPlaylists(User user, AuthenticationToken token)
        {
            return await GetUsersPlaylists(user.Id, token);
        }

        public async Task<Page<Playlist>> GetUsersPlaylists(string userId, AuthenticationToken token)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/users/" + userId + "/playlists", token).Result.Deserialize<Page<Playlist>>();
        }

        public async Task<Page<Playlist>> GetCurrentUsersPlaylists(string userId, AuthenticationToken token)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/me/playlists").Result.Deserialize<Page<Playlist>>();
        }

        public async Task<HttpStatusCode> AddTracks(string ownerId, string playlistId, List<Track> tracks, AuthenticationToken token)
        {
            var response =
                await HttpHelper.Instance.Put(
                    "https://api.spotify.com/v1/users/" + ownerId + "/playlists/" + playlistId + "/tracks?uris=" + tracks.Select(t => t.Uri).ToList().CreateCommaSeparatedList(), token);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> AddTrack(string ownerId, string playlistId, Track track, AuthenticationToken token)
        {
            var response =
                await HttpHelper.Instance.Put(
                    "https://api.spotify.com/v1/users/" + ownerId + "/playlists/" + playlistId + "/tracks?uris=" + track.Uri, token);
            return response.StatusCode;
        }
    }
}
