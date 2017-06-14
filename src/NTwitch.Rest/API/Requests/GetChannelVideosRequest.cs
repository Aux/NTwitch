using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetChannelVideosRequest : RestRequestBuilder
    {
        public GetChannelVideosRequest(ulong channelId, BroadcastType type, PageOptions paging) 
            : base("GET", $"channels/{channelId}/videos")
        {
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);

            if (type != BroadcastType.All)
                Parameters.Add("broadcast_type", type.ToString().ToLower());
        }
    }
}
