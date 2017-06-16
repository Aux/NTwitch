using NTwitch.Rest.Queue;
using System;

namespace NTwitch.Rest.API
{
    public class FindGamesRequest : RestRequestBuilder
    {
        public FindGamesRequest(string query, bool islive)
            : base("GET", "search/games")
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("live", islive);
        }
    }
}
