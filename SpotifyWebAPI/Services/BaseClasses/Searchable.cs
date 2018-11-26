using System.Threading.Tasks;
using System.Web;
using SpotifyWebAPI.Extensions;
using SpotifyWebAPI.Models;

namespace SpotifyWebAPI.Services.BaseClasses
{
    public abstract class Searchable<TEntity, TService> : Singleton<TService> where TService : new()
    {

        public static string BaseUrl => $"https://api.spotify.com/v1/{typeof(TEntity).Name.ToLower()}s/";

        public static async Task<Page<TEntity>> Search(
            string albumName = "",
            string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            var queryString = "";

            if (albumName != "")
                queryString += HttpUtility.HtmlEncode($" :album:{albumName}");
            if (artistName != "")
                queryString += HttpUtility.HtmlEncode($" :artist:{artistName}");
            if (year != "")
                queryString += HttpUtility.HtmlEncode($" :year:{year}");
            if (genre != "")
                queryString += HttpUtility.HtmlEncode($" :genre:{genre}");
            if (upc != "")
                queryString += HttpUtility.HtmlEncode($" :upc:{upc}");
            if (isrc != "")
                queryString += HttpUtility.HtmlEncode($" :isrc:{isrc}");

            queryString += "&limit=" + limit;
            queryString += "&offset=" + offset;
            queryString += $"&type={typeof(TEntity).Name.ToLower()}";

            return await HttpHelper.Instance.Get(queryString).Result.Deserialize<Page<TEntity>>();
        }

        public static async Task<TEntity> GetSpecific(string id)
        {
            return await HttpHelper.Instance.Get(BaseUrl + id).Result.Deserialize<TEntity>();
        }
    }
}
