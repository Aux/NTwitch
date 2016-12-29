using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestSelfChannel : RestChannel, ISelfChannel
    {
        public string Email { get; }
        public string StreamKey { get; }
        
        internal RestSelfChannel(TwitchRestClient client, ulong id) : base(client, id) { }

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
