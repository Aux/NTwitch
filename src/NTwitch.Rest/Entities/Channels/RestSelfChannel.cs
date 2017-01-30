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

        public RestSelfChannel(BaseRestClient client) : base(client) { }

        public static new RestSelfChannel Create(BaseRestClient client, string json)
        {
            var channel = new RestSelfChannel(client);
            JsonConvert.PopulateObject(json, channel);
            return channel;
        }

        // Posts
        public Task<RestPost> CreatePostAsync(Action<CreatePostParams> args)
            => ChannelHelper.CreatePostAsync(this, Client, args);
        public Task<RestPost> DeletePostAsync(uint postid)
            => ChannelHelper.DeletePostAsync(this, Client, postid);

        //Users
        public Task<IEnumerable<RestUser>> GetEditorsAsync()
            => ChannelHelper.GetEditorsAsync(this, Client);
        public Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync()
            => GetSubscribersAsync(false);
        public Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, PageOptions options = null)
            => ChannelHelper.GetSubscribersAsync(this, Client, ascending, options);
        public Task<RestUserSubscription> GetSubscriberAsync(uint userid)
            => ChannelHelper.GetSubscriberAsync(this, Client, userid);

        // SelfChannel
        public Task<RestSelfChannel> ModifyAsync(Action<ModifyChannelParams> args)
            => ChannelHelper.ModifyAsync(this, Client, args);
        public Task<RestSelfChannel> ResetStreamKeyAsync()
            => ChannelHelper.ResetStreamKeyAsync(this, Client);
        public Task StartCommercialAsync()
            => StartCommercialAsync(30);
        public Task StartCommercialAsync(int duration = 30)
            => ChannelHelper.StartCommercialAsync(this, Client, duration);
        
        // ISelfChannel
        Task ISelfChannel.ModifyAsync()
            => ModifyAsync(null);
        Task ISelfChannel.CreatePostAsync()
            => CreatePostAsync(null);
        Task ISelfChannel.DeletePostAsync(uint postId)
            => DeletePostAsync(postId);
        Task ISelfChannel.ResetStreamKeyAsync()
            => ResetStreamKeyAsync();
        Task ISelfChannel.StartCommercialAsync(int duration)
            => StartCommercialAsync(duration);
        Task ISelfChannel.GetEditorsAsync()
            => GetEditorsAsync();
        Task ISelfChannel.GetSubscribersAsync()
            => GetSubscribersAsync();
        Task ISelfChannel.GetSubscriberAsync(uint userId)
            => GetSubscriberAsync(userId);
    }
}
