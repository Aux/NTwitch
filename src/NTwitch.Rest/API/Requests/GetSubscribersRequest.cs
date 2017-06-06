using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetSubscribersRequest : RequestBuilder
    {
        public GetSubscribersRequest(ulong channelId, bool ascending, PageOptions paging) 
            : base("GET", $"channels/{channelId}/subscriptions")
        {
            SetParameter("direction", ascending ? "asc" : "desc");
            SetParameter("limit", paging.Limit);
            SetParameter("offset", paging.Offset);
        }
    }
}
