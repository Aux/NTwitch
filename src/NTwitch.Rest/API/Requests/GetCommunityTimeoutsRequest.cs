namespace NTwitch.Rest
{
    internal class GetCommunityTimeoutsRequest : RestRequest
    {
        public GetCommunityTimeoutsRequest(string id, uint limit) 
            : base("GET", $"communities/{id}/timeouts")
        {
            Parameters.Add("limit", limit);
        }
    }
}
