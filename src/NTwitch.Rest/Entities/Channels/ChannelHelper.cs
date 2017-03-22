using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ChannelHelper
    {
        public static async Task ModifyChannelAsync(RestSelfChannel channel, Action<ModifyChannelParams> options)
        {
            if (!channel.Client.Token.Authorization.Scopes.Contains("channel_editor"))
                throw new MissingScopeException("channel_editor");

            var model = await channel.Client.RestClient.ModifyChannelAsync(channel.Id, options);
            channel.Update(model);
        }
    }
}
