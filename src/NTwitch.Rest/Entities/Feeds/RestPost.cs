using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestPost : RestEntity, IPost
    {
        public string Body { get; }
        public IEnumerable<RestPostComment> Comments { get; }
        public DateTime CreatedAt { get; }
        public IEnumerable<RestPostEmote> Emotes { get; }
        public bool IsDeleted { get; }
        public RestPostPermissions Permissions { get; }
        public IEnumerable<RestPostReaction> Reactions { get; }
        public RestUser User { get; }
        
        public RestPost(TwitchRestClient client, ulong id) : base(client, id) { }

        public Task<RestPostComment> CreateCommentAsync(string content)
        {
            throw new NotImplementedException();
        }

        public Task CreateReactionAsync(ulong emoteid)
        {
            throw new NotImplementedException();
        }

        public Task<RestPost> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestPostComment> DeleteCommentAsync(ulong commentid)
        {
            throw new NotImplementedException();
        }

        public Task<RestPostComment> DeleteReactionAsync(ulong emoteid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestPostComment>> GetCommentsAsync(TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        //IPost

        IEnumerable<IPostComment> IPost.Comments
            => Comments;
        IEnumerable<IPostEmote> IPost.Emotes
            => Emotes;
        IPostPermissions IPost.Permissions
            => Permissions;
        IEnumerable<IPostReaction> IPost.Reactions
            => Reactions;
        IUser IPost.User
            => User;

        async Task<IPost> IPost.DeleteAsync()
            => await DeleteAsync();
        async Task IPost.CreateReactionAsync(ulong emoteid)
            => await CreateReactionAsync(emoteid);
        async Task IPost.DeleteReactionAsync(ulong emoteid)
            => await DeleteReactionAsync(emoteid);
        async Task<IEnumerable<IPostComment>> IPost.GetCommentsAsync(TwitchPageOptions options)
            => await GetCommentsAsync(options);
        async Task<IPostComment> IPost.CreateCommentAsync(string content)
            => await CreateCommentAsync(content);
        async Task<IPostComment> IPost.DeleteCommentAsync(ulong commentid)
            => await DeleteReactionAsync(commentid);
    }
}
