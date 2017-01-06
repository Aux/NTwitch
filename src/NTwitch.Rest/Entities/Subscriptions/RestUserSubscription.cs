using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription, IUserSubscription
    {
        [JsonProperty("")]
        public RestUser User { get; internal set; }

        public RestUserSubscription(TwitchRestClient client) : base(client) { }
        
        IUser IUserSubscription.User
            => User;
    }
}
