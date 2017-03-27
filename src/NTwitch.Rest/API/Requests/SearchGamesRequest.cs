using System;

namespace NTwitch.Rest
{
    internal class SearchGamesRequest : RestRequest
    {
        public SearchGamesRequest(string query, bool islive) 
            : base("GET", "search/games")
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("live", islive);
        }
    }
}