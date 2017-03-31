namespace NTwitch.Rest
{
    internal class SetCommunityAvatarRequest : RestRequest
    {
        public SetCommunityAvatarRequest(string token, string communityId, string image) 
            : base("POST", $"communities/{communityId}/images/avatar", token)
        {
            JsonBody = $"{{\"avatar_image\":\"{image}\"}}";
        }
    }
}
