using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class RestSelfUser : ISelfUser
    {
        public TwitchRestClient Client { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public uint Id { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public string Bio { get; }
        public string LogoUrl { get; }
        public string[] Links { get; }
        public bool IsPartnered { get; }
        public bool[] Notifications { get; }
        
        public Task<IEnumerable<string>> GetEmotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetStreamsAsync(int limit = 10, int page = 1, StreamType type = StreamType.All)
        {
            throw new NotImplementedException();
        }

        public Task GetVideosAsync(int limit = 10, int page = 1, BroadcastType type = BroadcastType.All)
        {
            throw new NotImplementedException();
        }

        public Task AddFollow(string name)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFollow(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IChannel>> GetFollowsAsync(int limit = 10, int page = 1, SortMode sort = SortMode.Ascending)
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
