using Newtonsoft.Json;

namespace SpotifyWebAPI.Models
{
    /// <summary>
    /// Spotify Pagainated Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
    {
        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// The requested data of type T
        /// </summary>
        [JsonProperty("items")]
        public T[] Items { get; set; }

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// URL to the next page of items. (null if none) 
        /// </summary>
        [JsonProperty("next")]
        public string Next { get; set; }

        /// <summary>
        /// The offset of the items returned (as set in the query or by default).
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// URL to the previous page of items. (null if none) 
        /// </summary>
        [JsonProperty("previous")]
        public string Previous { get; set; }

        /// <summary>
        /// The total number of items available to return. 
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// True if this object has another page
        /// </summary>
        public bool HasNextPage => Next != null;

        /// <summary>
        /// True if this object has a previous page
        /// </summary>
        public bool HasPreviousPage => Previous != null;

    }
}
