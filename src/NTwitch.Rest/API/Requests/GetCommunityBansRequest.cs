namespace NTwitch.Rest
{
    internal class GetCommunityBansRequest : RestRequest
    {
        public GetCommunityBansRequest(string id, uint limit) 
            : base("GET", $"communities/{id}/bans")
        {
            Parameters.Add("limit", limit);
        }
    }
}
