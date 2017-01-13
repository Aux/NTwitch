using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public Task<RestPost> CreatePostAsync(Action<CreatePostParams> args)
            => ChannelHelper.CreatePostAsync(this, args);

        public Task<RestPost> DeletePostAsync(ulong postid)
            => ChannelHelper.DeletePostAsync(this, postid);

        public Task<RestSelfChannel> ModifyAsync(Action<ModifyChannelParams> args)
            => ChannelHelper.ModifyAsync(this, args);

        public Task<IEnumerable<RestUser>> GetEditorsAsync()
            => ChannelHelper.GetEditorsAsynC(this);

        public Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(SortDirection direction = SortDirection.Descending, PageOptions options = null)
            => ChannelHelper.GetSubscribersAsync(this, direction, options);

        public Task<RestUserSubscription> GetSubscriberAsync(ulong userid)
            => ChannelHelper.GetSubscriberAsync(this, userid);

        public Task<RestSelfChannel> ResetStreamKeyAsync()
            => ChannelHelper.ResetStreamKeyAsync(this);

        public Task StartCommercialAsync(int duration = 30)
            => ChannelHelper.StartCommercialAsync(this, duration);
    }
}
