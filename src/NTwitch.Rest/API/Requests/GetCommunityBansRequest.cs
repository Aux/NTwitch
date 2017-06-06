namespace NTwitch.Rest
{
    internal class GetCommunityBansRequest : OldRestRequest
    {
        public GetCommunityBansRequest(string token, string id, uint limit) 
            : base("GET", $"communities/{id}/bans", token)
        {
            Parameters.Add("limit", limit);
        }
    }
}
