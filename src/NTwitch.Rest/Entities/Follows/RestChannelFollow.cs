using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow, IChannelFollow
    {
        public IChannel Channel { get; internal set; }

        internal RestChannelFollow(ITwitchClient client) : base(client) { }
    }
}
