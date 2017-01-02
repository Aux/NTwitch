using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestPostReaction : IEntity, IPostReaction
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public int Count { get; internal set; }
        public string EmoteName { get; internal set; }
        public string Name { get; internal set; }
        public ulong[] UserIds { get; internal set; }

        internal RestPostReaction(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
