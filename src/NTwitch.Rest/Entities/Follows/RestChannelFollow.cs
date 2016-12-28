namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow, IChannelFollow
    {
        public IChannel Channel { get; }

        internal RestChannelFollow(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
