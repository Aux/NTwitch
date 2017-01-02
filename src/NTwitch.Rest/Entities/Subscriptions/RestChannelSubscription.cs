using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription, IChannelSubscription
    {
        public IChannel Channel { get; internal set; }

        public RestChannelSubscription(ITwitchClient client) : base(client) { }

        //IChannelSubscription
        IChannel IChannelSubscription.Channel
            => Channel;
    }
}
