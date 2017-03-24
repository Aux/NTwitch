namespace NTwitch.Rest
{
    internal class GetChannelSubscriptionsRequest : RestRequest
    {
        public GetChannelSubscriptionsRequest(ulong channelId, bool ascending, uint limit, uint offset) 
            : base("GET", $"channels/{channelId}/subscriptions")
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
            Parameters.Add("direction", ascending ? "asc" : "desc");
        }
    }
}
