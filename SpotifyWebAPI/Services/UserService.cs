using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SpotifyWebAPI.Extensions;
using SpotifyWebAPI.Models;
using SpotifyWebAPI.Services.BaseClasses;

namespace SpotifyWebAPI.Services
{
    public class UserService : Singleton<UserService>
    {
        public async Task<User> GetUser(string userId)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/users/" + userId)
                .Result.Deserialize<User>();
        }

        public async Task<User> GetCurrentUserProfile(AuthenticationToken token)
        {
            return await HttpHelper.Instance.Get("https://api.spotify.com/v1/me", token)
                .Result.Deserialize<User>();
        }

        public async Task<Page<Track>> GetSavedTracks(AuthenticationToken token, int limit = 20,
            int offset = 0)
        {
            return await HttpHelper.Instance
                .Get("https://api.spotify.com/v1/me/tracks?limit=" + limit + "&offset=" + offset, token)
                .Result.Deserialize<Page<Track>>();
        }

        public async Task<HttpStatusCode> SaveTracks(List<string> trackIds, AuthenticationToken token)
        {
            var response =
                await HttpHelper.Instance.Put(
                    "https://api.spotify.com/v1/me/tracks?ids=" + trackIds.CreateCommaSeparatedList(), token);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteTracks(List<string> trackIds, AuthenticationToken token)
        {
            var response =
                await HttpHelper.Instance.Delete(
                    "https://api.spotify.com/v1/me/tracks?ids=" + trackIds.CreateCommaSeparatedList(), token);
            return response.StatusCode;
        }

        public async Task<Dictionary<string, bool>> AreSaved(List<string> trackIds, AuthenticationToken token)
        {
            bool[] results = await HttpHelper.Instance.Get("https://api.spotify.com/v1/me/tracks/contains?ids=" + trackIds.CreateCommaSeparatedList(), token).Result.Deserialize<bool[]>();

            Dictionary<string, bool> trackResults = new Dictionary<string, bool>();

            for (int t = 0; t < trackIds.Count; t++)
            {   
                trackResults.Add(trackIds[t], results[t]);
            }

            return trackResults;
        }

        public async Task<bool> IsSaved(string trackId, AuthenticationToken token)
        {
            var result = await AreSaved(new List<string>{ trackId }, token);
            return result.FirstOrDefault().Value;
        }
    }
}
