using Newtonsoft.Json;

namespace NTwitch.Rest
{
    internal class ModifyChannelRequest : RestRequest
    {
        public ModifyChannelRequest(ulong channelId, ModifyChannel changes) 
            : base("PUT", $"channels/{channelId}")
        {
            JsonBody = JsonConvert.SerializeObject(changes);
        }
    }
}
