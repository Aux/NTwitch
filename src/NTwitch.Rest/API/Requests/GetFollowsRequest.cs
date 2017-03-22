namespace NTwitch.Rest
{
    internal class GetFollowsRequest : RestRequest
    {
        public GetFollowsRequest(ulong userId, SortMode sort, bool ascending, uint limit, uint offset) 
            : base("GET", $"users/{userId}/follows/channels")
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
            Parameters.Add("direction", ascending ? "desc" : "asc");
            Parameters.Add("sortby", "created_at");
        }
    }
}