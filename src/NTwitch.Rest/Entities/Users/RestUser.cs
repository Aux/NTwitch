using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestUser : IUser
    {
        public TwitchRestClient Client { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public uint Id { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Bio { get; }
        public string LogoUrl { get; }
        public TwitchLinks Links { get; }
        
        public Task<IChannelFollow> GetFollowAsync(string channel)
        {
            throw new NotImplementedException();
        }

        public Task<IChannelFollow> GetFollowAsync(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IChannel>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, SortDirection direction = SortDirection.Ascending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
            => DisplayName;
    }
}
