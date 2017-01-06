using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public static class RestChannelExtensions
    {
        public static Task<IEnumerable<PubsubTopic>> GetTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public static Task ListenAsync(PubsubTopic topic)
        {
            throw new NotImplementedException();
        }

        public static Task UnlistenAsync(PubsubTopic topic)
        {
            throw new NotImplementedException();
        }
    }
}
