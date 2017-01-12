using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestFollow : FollowBase
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        internal RestFollow(TwitchRestClient client) : base(client) { }

        internal static RestFollow Create(TwitchRestClient client, string json)
        {
            var follow = new RestFollow(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, follow);
            return follow;
        }
    }
}
