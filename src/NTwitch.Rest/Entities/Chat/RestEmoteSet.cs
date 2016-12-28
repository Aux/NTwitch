using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestEmoteSet : RestEntity, IEmoteSet
    {
        public IEnumerable<IEmote> Emotes { get; }

        internal RestEmoteSet(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
