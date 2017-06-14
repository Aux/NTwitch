using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetChannelFollowersRequest : RestRequestBuilder
    {
        public GetChannelFollowersRequest(ulong channelId, bool ascending, PageOptions paging)
            : base("GET", $"channels/{channelId}/follows")
        {
            Parameters.Add("direction", ascending ? "desc" : "asc");
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
