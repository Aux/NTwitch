namespace NTwitch.Rest
{
    internal class SetCommunityCoverRequest : RestRequest
    {
        public SetCommunityCoverRequest(string communityId, string image) 
            : base("POST", $"communities/{communityId}/images/cover")
        {
            JsonBody = $"{{\"cover_image\":\"{image}\"}}";
        }
    }
}
