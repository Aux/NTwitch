using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetSubscribersRequest : RestRequestBuilder
    {
        public GetSubscribersRequest(ulong channelId, bool ascending, PageOptions paging) 
            : base("GET", $"channels/{channelId}/subscriptions")
        {
            Parameters.Add("direction", ascending ? "asc" : "desc");
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
