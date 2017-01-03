using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestSelfChannel : RestChannel, ISelfChannel
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [JsonProperty("stream_key")]
        public string StreamKey { get; internal set; }

        internal RestSelfChannel(ITwitchClient client) : base(client) { }

        public Task<RestPost> CreatePostAsync(Action<CreatePostParams> args)
        {
            throw new NotImplementedException();
        }

        public Task<RestPost> DeletePostAsync(ulong postid)
        {
            throw new NotImplementedException();
        }

        public Task<RestSelfChannel> ModifyAsync(Action<ModifyChannelParams> args)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestUser>> GetEditorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<RestUserSubscription> GetSubscriberAsync(ulong userid)
        {
            throw new NotImplementedException();
        }

        public Task<RestSelfChannel> ResetStreamKeyAsync()
        {
            throw new NotImplementedException();
        }

        //ISelfChannel

        public Task StartCommercialAsync(int duration = 30)
        {
            throw new NotImplementedException();
        }

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
