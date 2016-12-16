using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestSelfChannel : ISelfChannel
    {
        public string Background { get; }
        public string BannerUrl { get; }
        public string BroadcasterLanguage { get; }
        public DateTime CreatedAt { get; }
        public int Delay { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public int Followers { get; }
        public string Game { get; }
        public uint Id { get; }
        public bool IsMature { get; }
        public bool IsPartner { get; }
        public string Language { get; }
        public string[] Links { get; }
        public string LogoUrl { get; }
        public string Name { get; }
        public string ProfileBackground { get; }
        public string ProfileBannerUrl { get; }
        public string Status { get; }
        public string StreamKey { get; }
        public DateTime UpdatedAt { get; }
        public string Url { get; }
        public string VideoBannerUrl { get; }
        public int Views { get; }

        public Task<IBlock> CreateBlockAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IPost> CreatePostAsync(Action<CreatePostParams> properties)
        {
            throw new NotImplementedException();
        }

        public Task<IBlock> DeleteBlockAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBadge>> GetBadgesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBlock>> GetBlocksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetEditorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IUserFollow> GetFollowAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUserFollow>> GetFollowersAsync(SortDirection sort = SortDirection.Descending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IPost> GetPostAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IPost>> GetPostsAsync(int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IUserSubscription> GetSubscriberAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUserSubscription>> GetSubscribersAsync(SortDirection sort = SortDirection.Descending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IVideo>> GetVideosAsync(bool broadcasts = false, bool hls = false, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(Action<ModifyChannelParams> properties)
        {
            throw new NotImplementedException();
        }

        public Task ResetStreamKeyAsync()
        {
            throw new NotImplementedException();
        }

        public Task StartCommercialAsync(int length = 30)
        {
            throw new NotImplementedException();
        }
    }
}
