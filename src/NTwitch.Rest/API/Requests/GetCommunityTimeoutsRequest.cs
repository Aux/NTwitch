namespace NTwitch.Rest
{
    internal class GetCommunityTimeoutsRequest : OldRestRequest
    {
        public GetCommunityTimeoutsRequest(string token, string id, uint limit) 
            : base("GET", $"communities/{id}/timeouts", token)
        {
            Parameters.Add("limit", limit);
        }
    }
}
