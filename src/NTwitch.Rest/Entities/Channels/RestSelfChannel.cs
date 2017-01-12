using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestSelfChannel : RestChannel
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [JsonProperty("stream_key")]
        public string StreamKey { get; internal set; }

        public RestSelfChannel(TwitchRestClient client) : base(client) { }

        public static new RestSelfChannel Create(BaseRestClient client, string json)
        {
            var channel = new RestSelfChannel(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        public async Task<RestPost> CreatePostAsync(Action<CreatePostParams> args)
            => await ChannelHelper.CreatePostAsync(this, args);

        public async Task<RestPost> DeletePostAsync(ulong postid)
            => await ChannelHelper.DeletePostAsync(this, postid);

        public async Task<RestSelfChannel> ModifyAsync(Action<ModifyChannelParams> args)
            => await ChannelHelper.ModifyAsync(this, args);

        public async Task<IEnumerable<RestUser>> GetEditorsAsync()
            => await ChannelHelper.GetEditorsAsynC(this);

        public async Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
            => await ChannelHelper.GetSubscribersAsync(this, direction, options);

        public async Task<RestUserSubscription> GetSubscriberAsync(ulong userid)
            => await ChannelHelper.GetSubscriberAsync(this, userid);

        public async Task<RestSelfChannel> ResetStreamKeyAsync()
            => await ChannelHelper.ResetStreamKeyAsync(this);

        public async Task StartCommercialAsync(int duration = 30)
            => await ChannelHelper.StartCommercialAsync(this, duration);
    }
}
