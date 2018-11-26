using System.Collections.Generic;

namespace SpotifyWebAPI.Extensions
{
    public static class StringExtensions
    {



        public static string CreateCommaSeparatedList(this List<string> strings)
        {
            var output = string.Empty;
            foreach (var str in strings)
                output += str + ",";
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        public static string CreateKeyValueAmpersandSeparatedList(this Dictionary<string, string> keyValuePairs)
        {
            var output = string.Empty;
            foreach (var str in keyValuePairs)
                output += str.Key + "=" + str.Value + "&";
            output = output.Substring(0, output.Length - 1);

            return output;
        }

    }
}
