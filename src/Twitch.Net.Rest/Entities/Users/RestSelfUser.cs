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

        public Task GetEmotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetStreamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetVideosAsync()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
            => DisplayName;
    }
}
