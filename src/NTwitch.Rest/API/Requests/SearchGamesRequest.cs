using System;

namespace NTwitch.Rest
{
    internal class SearchGamesRequest : OldRestRequest
    {
        public SearchGamesRequest(string token, string query, bool islive) 
            : base("GET", "search/games", token)
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("live", islive);
        }
    }
}