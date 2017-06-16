using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowsRequest : RestRequestBuilder
    {
        public GetFollowsRequest(ulong userId, SortMode sort, bool ascending, PageOptions paging)
            : base("GET", $"users/{userId}/follows/channels")
        {
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
            Parameters.Add("direction", ascending ? "desc" : "asc");
            Parameters.Add("sortby", "created_at");
        }
    }
}
