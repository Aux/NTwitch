using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestChannel : IChannel
    {
        public string Background { get; }
        public string BannerUrl { get; }
        public string BroadcasterLanguage { get; }
        public DateTime CreatedAt { get; }
        public int Delay { get; }
        public string DisplayName { get; }
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
        public DateTime UpdatedAt { get; }
        public string Url { get; }
        public string VideoBannerUrl { get; }
        public int Views { get; }

        public Task<IEnumerable<IBadge>> GetBadgesAsync()
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
    }
}
