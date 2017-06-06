using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowsRequest : RequestBuilder
    {
        public GetFollowsRequest(ulong userId, SortMode sort, bool ascending, PageOptions paging)
            : base("GET", $"users/{userId}/follows/channels")
        {
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
            _endpointParams.Add("direction", ascending ? "desc" : "asc");
            _endpointParams.Add("sortby", "created_at");
        }
    }
}
