using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpotifyWebAPI.Services
{

    internal sealed class HttpHelper
    {
        private static readonly Lazy<HttpHelper> Lazy =
            new Lazy<HttpHelper>(() => new HttpHelper());

        public static HttpHelper Instance => Lazy.Value;

        private readonly HttpClient _client;

        private HttpHelper()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Content-Type", "application/json, application/x-www-form-urlencoded");

        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> Get(string url, AuthenticationToken token, bool includeBearer = true)
        {
            IncludeBearer(includeBearer, token);

            return await Get(url);
        }

        public async Task<HttpResponseMessage> GetToken(string url, Dictionary<string, string> postData, string clientId, string clientSecret)
        {
            HttpContent content = EncodeContent(postData);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes(
                            $"{clientId}:{clientSecret}")));

            return await _client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> postData = null, string jsonString = null)
        {
            HttpContent content = EncodeContent(postData, jsonString);

            return await _client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Post(string url, AuthenticationToken token, Dictionary<string, string> postData = null, bool includeBearer = true)
        {
            IncludeBearer(includeBearer, token);

            return await Post(url, postData);
        }

        public async Task<HttpResponseMessage> Post(string url, AuthenticationToken token, string jsonString, bool includeBearer = true)
        {
            IncludeBearer(includeBearer, token);

            return await Post(url, null, jsonString);
        }

        public async Task<HttpResponseMessage> Put(string url, AuthenticationToken token, Dictionary<string, string> postData = null, string jsonString = null, bool includeBearer = true)
        {
            HttpContent content = EncodeContent(postData, jsonString);
            IncludeBearer(includeBearer, token);

            return await _client.PutAsync(url, content);
        }

        public async Task<HttpResponseMessage> Delete(string url, AuthenticationToken token, bool includeBearer = true)
        {
            IncludeBearer(includeBearer, token);

            return await _client.DeleteAsync(url);
        }

        private HttpContent EncodeContent(Dictionary<string, string> postData, string jsonString = null)
        {
            if (!string.IsNullOrWhiteSpace(jsonString))
                return new StringContent(jsonString);

            return postData != null
                ? new FormUrlEncodedContent(postData.ToArray())
                : null;
        }

        private void IncludeBearer(bool includeBearer, AuthenticationToken token)
        {
            _client.DefaultRequestHeaders.Authorization = includeBearer
                ? new AuthenticationHeaderValue("Bearer", token.AccessToken)
                : new AuthenticationHeaderValue(token.AccessToken);
        }
    }
}
