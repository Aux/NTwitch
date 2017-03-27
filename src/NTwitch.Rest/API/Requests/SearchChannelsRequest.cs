using System;

namespace NTwitch.Rest
{
    internal class SearchChannelsRequest : RestRequest
    {
        public SearchChannelsRequest(string query, uint limit, uint offset) 
            : base("GET", "search/games")
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
        }
    }
}