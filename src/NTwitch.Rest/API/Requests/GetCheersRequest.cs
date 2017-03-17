using System.Collections.Generic;

namespace NTwitch.Rest
{
    internal class GetCheersRequest : RestRequest
    {
        public GetCheersRequest(ulong? channelId) 
            : base("GET", "bits/actions", new Dictionary<string, object>()
        {
            { "channel_id", channelId }
        }) { }
    }
}