using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class ModifyChannelRequest : JsonRequestBuilder
    {
        public ModifyChannelRequest(ulong channelId, ModifyChannelParams changes)
            : base("PUT", $"channels/{channelId}", changes) { }
    }
}
