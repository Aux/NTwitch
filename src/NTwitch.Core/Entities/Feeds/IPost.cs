using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IPost : IEntity
    {
        string Body { get; }
        IEnumerable<IPostComment> Comments { get; }
        DateTime CreatedAt { get; }
        bool IsDeleted { get; }
        IEnumerable<IPostEmote> Emotes { get; }
        IPostPermissions Permissions { get; }
        IEnumerable<IPostReaction> Reactions { get; }
        IUser User { get; set; }

        /// <summary> Deletes this post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task<IPost> DeleteAsync();
        /// <summary> Creates a reaction to this post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task CreateReactionAsync(ulong emoteid);
        /// <summary> Deletes a specified reaction to this post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task DeleteReactionAsync(ulong emoteid);
        /// <summary> Gets all comments on a specified post in a specified channel feed. </summary>
        Task<IEnumerable<IPostComment>> GetCommentsAsync(TwitchPageOptions options = null);
        /// <summary> Creates a comment to this post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task<IPostComment> CreateCommentAsync(string content);
        /// <summary> Deletes a specified comment on this post in the channel feed. </summary> 
        /// <remarks> Required scope: channel_feed_edit </remarks>
        Task<IPostComment> DeleteCommentAsync(ulong id);
    }
}
