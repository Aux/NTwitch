using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestSelfChannel : RestChannel, ISelfChannel
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [JsonProperty("stream_key")]
        public string StreamKey { get; internal set; }

        internal RestSelfChannel(TwitchRestClient client) : base(client) { }

        internal static new RestSelfChannel Create(BaseTwitchClient client, string json)
        {
            var channel = new RestSelfChannel(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        internal RestSelfChannel Update()
        {
            return null;
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

        ITwitchClient IEntity.Client
            => Client;
        async Task<IPost> ISelfChannel.CreatePostAsync(Action<CreatePostParams> args)
            => await CreatePostAsync(args);
        async Task<IPost> ISelfChannel.DeletePostAsync(ulong postid)
            => await DeletePostAsync(postid);
        async Task<ISelfChannel> ISelfChannel.ModifyAsync(Action<ModifyChannelParams> args)
            => await ModifyAsync(args);
        async Task<IEnumerable<IUser>> ISelfChannel.GetEditorsAsync()
            => await GetEditorsAsync();
        async Task<IEnumerable<IUserSubscription>> ISelfChannel.GetSubscribersAsync(SortDirection direction, TwitchPageOptions options)
            => await GetSubscribersAsync(direction, options);
        async Task<IUserSubscription> ISelfChannel.GetSubscriberAsync(ulong userid)
            => await GetSubscriberAsync(userid);
        async Task<ISelfChannel> ISelfChannel.ResetStreamKeyAsync()
            => await ResetStreamKeyAsync();
    }
}
