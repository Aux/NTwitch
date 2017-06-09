using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetStreamRequest : RestRequestBuilder
    {
        public GetStreamRequest(ulong channelId, StreamType type)
            : base("GET", $"streams/{channelId}")
        {
            Parameters.Add("stream_type", type.ToString().ToLower());
        }
    }
}
