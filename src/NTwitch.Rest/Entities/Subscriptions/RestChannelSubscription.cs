namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription, IChannelSubscription
    {
        public IChannel Channel { get; }

        public RestChannelSubscription(TwitchRestClient client, ulong id) : base(client, id) { }

        //IChannelSubscription
        IChannel IChannelSubscription.Channel
            => Channel;
    }
}
