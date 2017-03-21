using System;
using System.Threading.Tasks;

namespace NTwitch.Tcp
{
    public static class ChannelExtensions
    {
        public static Task<object> JoinAsync(this IChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<object> LeaveAsync(this IChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<object> SendMessageAsync(this IChannel channel, string content)
        {
            throw new NotImplementedException();
        }
    }
}
