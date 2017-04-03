using System;

namespace NTwitch.Rest
{
    internal class SearchStreamsRequest : RestRequest
    {
        public SearchStreamsRequest(string token, string query, bool? hls, uint limit, uint offset) 
            : base("GET", "search/streams", token)
        {
            if (hls != null)
                Parameters.Add("hls", hls);

            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);

        }
    }
}