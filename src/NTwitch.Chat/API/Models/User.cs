using System.Collections.Generic;

namespace NTwitch.Chat.API
{
    internal class User
    {
        // user-id tag
        public ulong Id { get; set; }
        // prefix url
        public string Name { get; set; }
        // display-name tag
        public string DisplayName { get; set; }
        // subscriber tag
        public bool IsSubscriber { get; set; }
        // mod tag
        public bool IsModerator { get; set; }
        // turbo tag
        public bool IsTurbo { get; set; }
        // user-type tag
        public string Type { get; set; }
        // badges tag
        public Dictionary<string, bool> Badges { get; set; }
    }
}
