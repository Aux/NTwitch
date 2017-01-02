using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestIngest : IEntity, IIngest
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public double Availability { get; internal set; }
        public bool IsDefault { get; internal set; }
        public string Name { get; internal set; }
        public string UrlTemplate { get; internal set; }

        internal RestIngest(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
