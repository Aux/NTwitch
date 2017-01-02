using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class PubsubChannel : RestChannel
    {
        public new TwitchPubsubClient Client { get; }

        internal PubsubChannel(ITwitchClient client) : base(client)
        {
            Client = client as TwitchPubsubClient;
        }
    }
}
