using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestEmoteImage : IEntity, IEmoteImage
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public int Height { get; internal set; }
        public string ImageUrl { get; internal set; }
        public ulong SetId { get; internal set; }
        public int Width { get; internal set; }

        internal RestEmoteImage(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
