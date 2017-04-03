namespace NTwitch.Rest
{
    internal class SetCommunityCoverRequest : RestRequest
    {
        public SetCommunityCoverRequest(string token, string communityId, string image) 
            : base("POST", $"communities/{communityId}/images/cover", token)
        {
            JsonBody = $"{{\"cover_image\":\"{image}\"}}";
        }
    }
}
