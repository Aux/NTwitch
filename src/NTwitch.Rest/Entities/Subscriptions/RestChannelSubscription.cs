using System.Runtime.CompilerServices;


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
