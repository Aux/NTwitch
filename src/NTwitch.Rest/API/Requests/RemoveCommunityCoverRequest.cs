namespace NTwitch.Rest
{
    internal class RemoveCommunityCoverRequest : RestRequest
    {
        public RemoveCommunityCoverRequest(string communityId) 
            : base("DELETE", $"communities/{communityId}/images/cover") { }
    }
}
