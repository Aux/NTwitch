using NTwitch.Rest.Queue;
using System;

namespace NTwitch.Rest.API
{
    public class FindGamesRequest : RequestBuilder
    {
        public FindGamesRequest(string query, bool islive)
            : base("GET", "search/games")
        {
            _endpointParams.Add("query", Uri.EscapeDataString(query));
            _endpointParams.Add("live", islive);
        }
    }
}
