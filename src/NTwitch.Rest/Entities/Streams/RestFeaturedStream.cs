using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestFeaturedStream : IEntity, IFeaturedStream
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string ImageUrl { get; internal set; }
        public bool IsScheduled { get; internal set; }
        public bool IsSponsored { get; internal set; }
        public int Priority { get; internal set; }
        public RestStream Stream { get; internal set; }
        public string Text { get; internal set; }
        public string Title { get; internal set; }

        internal RestFeaturedStream(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
        IStream IFeaturedStream.Stream
            => Stream;
    }
}
