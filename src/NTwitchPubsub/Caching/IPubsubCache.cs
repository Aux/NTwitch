using System.Collections.Generic;

namespace NTwitch.Pubsub
{
    public interface IPubsubCache : ICache
    {
        new IReadOnlyCollection<PubsubChannel> Channels { get; }
        new IReadOnlyCollection<PubsubUser> Users { get; }

        new PubsubChannel GetChannel(ulong channelId);
        new PubsubUser GetUser(ulong userId);

        void AddChannel(PubsubChannel channel);
        void AddUser(PubsubUser user);

        new PubsubChannel RemoveChannel(ulong channelId);
        new PubsubUser RemoveUser(ulong userId);
    }
}
