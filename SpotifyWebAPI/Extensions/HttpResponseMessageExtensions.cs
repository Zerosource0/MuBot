using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyWebAPI.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            using (var jsonResult = response.Content.ReadAsStringAsync())
            {
                return JsonConvert.DeserializeObject<T>(await jsonResult, SerializerSettings);
            }
        }
    }
}
