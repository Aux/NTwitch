using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitch.Rest
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
        public string[] Links { get; }
        
        public Task<IEnumerable<IChannel>> GetFollowsAsync(int limit = 10, int page = 1, SortDirection sort = SortDirection.Ascending)
        {
            throw new NotImplementedException();
        }

        public Task<IChannel> GetFollow(string name)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
            => DisplayName;
    }
}
