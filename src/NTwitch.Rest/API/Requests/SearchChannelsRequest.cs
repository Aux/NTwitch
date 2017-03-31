using System;

namespace NTwitch.Rest
{
    internal class SearchChannelsRequest : RestRequest
    {
        public SearchChannelsRequest(string token, string query, uint limit, uint offset) 
            : base("GET", "search/games", token)
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
        }
    }
}