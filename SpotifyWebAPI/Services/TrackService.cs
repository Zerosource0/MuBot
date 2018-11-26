using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyWebAPI.Extensions;
using SpotifyWebAPI.Models;
using SpotifyWebAPI.Services.BaseClasses;

namespace SpotifyWebAPI.Services
{
    public class TrackService : Searchable<Track, TrackService>
    {
        public static async Task<List<Track>> GetMultiple(List<string> ids)
        {
            return await HttpHelper.Instance.Get(BaseUrl + ids).Result.Deserialize<List<Track>>();
        }
    }
}
