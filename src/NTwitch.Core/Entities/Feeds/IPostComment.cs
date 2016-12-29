using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IPostComment : IEntity
    {
        string Body { get; }
        DateTime CreatedAt { get; }
        bool IsDeleted { get; }
        IEnumerable<IEmote> Emotes { get; }
        IPostPermissions Permissions { get; }
        IEnumerable<IPostReaction> Reactions { get; }
        IUser User { get; }

        /// <summary> Creates a reaction to this comment on a post in a channel feed </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task CreateReactionAsync(ulong emoteid);
        /// <summary> Deletes a reaction to this comment on a post in a channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task DeleteReactionAsync(ulong emoteid);
    }
}
