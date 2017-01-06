using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow, IUserFollow
    {
        [JsonProperty("")]
        public IUser User { get; internal set; }

        internal RestUserFollow(TwitchRestClient client) : base(client) { }
    }
}
