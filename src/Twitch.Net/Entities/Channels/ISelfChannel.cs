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

        Task<IEnumerable<IUser>> GetEditorsAsync();
        Task<IEnumerable<IPost>> GetPostsAsync(int limit = 10, int page = 1);
        Task<IPost> GetPostAsync(uint id);
        Task<IPost> CreatePostAsync(Action<CreatePostParams> properties);
        Task<IEnumerable<IBlock>> GetBlocksAsync();
        Task<IBlock> CreateBlockAsync(string name);
        Task<IBlock> DeleteBlockAsync(string name);

        Task ModifyAsync(Action<ModifyChannelParams> properties);
        Task ResetStreamKeyAsync();
        Task StartCommercialAsync(int length = 30);
    }
}
