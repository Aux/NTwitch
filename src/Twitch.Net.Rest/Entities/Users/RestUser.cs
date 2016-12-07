using System;

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
        
        public override string ToString()
            => DisplayName;
    }
}
