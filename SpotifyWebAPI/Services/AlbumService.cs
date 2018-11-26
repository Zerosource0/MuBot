using System;
using System.Threading.Tasks;
using SpotifyWebAPI.Extensions;
using SpotifyWebAPI.Models;
using SpotifyWebAPI.Services.BaseClasses;

namespace SpotifyWebAPI.Services
{
    public class AlbumService : Searchable<Album, AlbumService>
    {
        public async Task<Page<Album>> GetArtistAlbums(string artistId)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/artists/" + artistId + "/albums").Result.Deserialize<Page<Album>>();
        }

        public async Task<Page<Track>> GetAlbumTracks(string albumId, int limit = 20, int offset = 0)
        {
            return await HttpHelper.Instance.Get(BaseUrl + albumId + "/tracks?limit=" + limit + "&offset=" + offset).Result.Deserialize<Page<Track>>(); ;
        }
    }
}
