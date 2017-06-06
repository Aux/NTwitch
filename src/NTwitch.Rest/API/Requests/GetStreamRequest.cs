using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetStreamRequest : RequestBuilder
    {
        public GetStreamRequest(ulong channelId, StreamType type)
            : base("GET", $"streams/{channelId}")
        {
            _endpointParams.Add("stream_type", type.ToString().ToLower());
        }
    }
}
