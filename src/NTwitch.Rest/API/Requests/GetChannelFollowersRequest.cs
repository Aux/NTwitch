namespace NTwitch.Rest
{
    internal class GetChannelFollowersRequest : RestRequest
    {
        public GetChannelFollowersRequest(string token, ulong channelId, bool ascending, uint limit, uint offset) 
            : base("GET", $"channels/{channelId}/follows", token)
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
            Parameters.Add("direction", ascending ? "asc" : "desc");
        }
    }
}
