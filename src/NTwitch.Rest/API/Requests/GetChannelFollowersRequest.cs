namespace NTwitch.Rest
{
    internal class GetChannelFollowersRequest : RestRequest
    {
        public GetChannelFollowersRequest(ulong channelId, bool ascending, uint limit, uint offset) 
            : base("GET", $"channels/{channelId}/follows")
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
            Parameters.Add("direction", ascending ? "asc" : "desc");
        }
    }
}
