using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyWebAPI.Extensions;
using SpotifyWebAPI.Models;
using SpotifyWebAPI.Services.BaseClasses;

namespace SpotifyWebAPI.Services
{
    public class ArtistService : Searchable<Artist, ArtistService>
    {
        public async Task<Page<AlbumType>> GetArtistAlbums(string artistId)
        {
            return await HttpHelper.Instance.Get(BaseUrl + artistId + "/albums").Result.Deserialize<Page<AlbumType>>();
        }

        public async Task<List<Track>> GetTopTracks(string artistId, string countryCode = "DK")
        {
            if (countryCode == null) throw new ArgumentNullException(nameof(countryCode));

            return await HttpHelper.Instance.Get(BaseUrl + artistId + "/top-tracks?country=" + countryCode).Result.Deserialize<List<Track>>();
        }

    }
}
