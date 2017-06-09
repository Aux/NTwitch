using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowedStreamsRequest : RestRequestBuilder
    {
        public GetFollowedStreamsRequest(StreamType type, PageOptions paging)
            : base("GET", "streams/followed")
        {
            Parameters.Add("type", type.ToString().ToLower());
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
