using Newtonsoft.Json;

namespace NTwitch.Rest
{
    internal class ModifyChannelRequest : RestRequest
    {
        public ModifyChannelRequest(string token, ulong channelId, ModifyChannel changes) 
            : base("PUT", $"channels/{channelId}", token)
        {
            JsonBody = JsonConvert.SerializeObject(changes);
        }
    }
}
