using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface IPost
    {
        DateTime CreatedAt { get; }

        uint Id { get; }
        IPostUser Author { get; }
        string Body { get; }
        string[] Emotes { get; }
        IEnumerable<IPostReaction> Reactions { get; }
        bool IsDeleted { get; }

        Task DeleteAsync();
        Task CreateReactionAsync(uint id);
        Task DeleteReactionAsync(uint id);
    }
}
