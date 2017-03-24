namespace NTwitch.Rest
{
    internal class RemoveCommunityAvatarRequest : RestRequest
    {
        public RemoveCommunityAvatarRequest(string communityId) 
            : base("DELETE", $"communities/{communityId}/images/avatar") { }
    }
}
