using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetSubscribersRequest : RequestBuilder
    {
        public GetSubscribersRequest(ulong channelId, bool ascending, PageOptions paging) 
            : base("GET", $"channels/{channelId}/subscriptions")
        {
            _endpointParams.Add("direction", ascending ? "asc" : "desc");
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
