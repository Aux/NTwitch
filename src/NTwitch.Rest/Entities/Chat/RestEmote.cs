using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestEmote : RestEntity, IEmote
    {
        public IEnumerable<IEmoteImage> Images { get; }
        public string Name { get; }

        internal RestEmote(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
