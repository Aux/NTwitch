using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestStreamSummary : IEntity, IStreamSummary
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public int Channels { get; internal set; }
        public int Viewers { get; internal set; }

        internal RestStreamSummary(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
