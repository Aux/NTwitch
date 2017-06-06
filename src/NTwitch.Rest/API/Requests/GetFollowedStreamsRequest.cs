using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowedStreamsRequest : RequestBuilder
    {
        public GetFollowedStreamsRequest(StreamType type, PageOptions paging)
            : base("GET", "streams/followed")
        {
            _endpointParams.Add("type", type.ToString().ToLower());
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
