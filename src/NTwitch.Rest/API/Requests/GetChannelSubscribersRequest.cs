namespace NTwitch.Rest
{
    internal class GetChannelSubscribersRequest : RestRequest
    {
        public GetChannelSubscribersRequest(string token, ulong channelId, bool ascending, uint limit, uint offset) 
            : base("GET", $"channels/{channelId}/subscriptions", token)
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
            Parameters.Add("direction", ascending ? "asc" : "desc");
        }
    }
}
