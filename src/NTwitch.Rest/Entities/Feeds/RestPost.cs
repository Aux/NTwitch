using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestPost : IEntity, IPost
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string Body { get; internal set; }
        public IEnumerable<RestPostComment> Comments { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public IEnumerable<RestPostEmote> Emotes { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public RestPostPermissions Permissions { get; internal set; }
        public IEnumerable<RestPostReaction> Reactions { get; internal set; }
        public RestUser User { get; internal set; }

        internal RestPost(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

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

        ITwitchClient IEntity.Client
            => Client;
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
