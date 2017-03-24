namespace NTwitch.Rest
{
    internal class SetCommunityAvatarRequest : RestRequest
    {
        public SetCommunityAvatarRequest(string communityId, string image) 
            : base("POST", $"communities/{communityId}/images/avatar")
        {
            JsonBody = $"{{\"avatar_image\":\"{image}\"}}";
        }
    }
}
