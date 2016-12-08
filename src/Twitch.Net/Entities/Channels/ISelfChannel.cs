using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface ISelfChannel : IChannel
    {
        string Email { get; }
        string StreamKey { get; }

        Task GetEditorsAsync();
        Task ModifyAsync(Action<ModifyChannelParams> properties);
        Task ResetStreamKeyAsync();
        Task StartCommercialAsync(int length = 30);
        Task GetPostsAsync(int limit = 10, string cursor = null);
    }
}
