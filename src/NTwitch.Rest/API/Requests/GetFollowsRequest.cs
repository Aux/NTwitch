using System.Collections.Generic;

namespace NTwitch.Rest
{
    internal class GetFollowsRequest : RestRequest
    {
        public GetFollowsRequest(ulong userId, SortMode sort, bool ascending, int limit, int offset) 
            : base("GET", $"users/{userId}/follows/channels", new Dictionary<string, object>()
        {
            { "limit", limit },
            { "offset", offset },
            { "direction", ascending ? "desc" : "asc" },
            { "sortby", "created_at" }
        }) { }
    }
}