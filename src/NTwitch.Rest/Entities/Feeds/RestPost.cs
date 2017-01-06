using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestPost : IEntity, IPost
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public string Body { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<RestPostComment> Comments { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<RestPostEmote> Emotes { get; internal set; }
        [JsonProperty("")]
        public bool IsDeleted { get; internal set; }
        [JsonProperty("")]
        public RestPostPermissions Permissions { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<RestPostReaction> Reactions { get; internal set; }
        [JsonProperty("")]
        public RestUser User { get; internal set; }

        internal RestPost(TwitchRestClient client)
        {
            Client = client;
        }

        public async Task<RestPostComment> CreateCommentAsync(string content)
            => await PostHelper.CreateCommentAsync(this, content);

        public async Task CreateReactionAsync(ulong emoteid)
            => await PostHelper.CreateReactionAsync(this, emoteid);

        public async Task<RestPost> DeleteAsync()
            => await PostHelper.DeleteAsync(this);

        public async Task<RestPostComment> DeleteCommentAsync(ulong commentid)
            => await PostHelper.DeleteCommentAsync(this, commentid);

        public async Task<RestPostComment> DeleteReactionAsync(ulong emoteid)
            => await PostHelper.DeleteReactionAsync(this, emoteid);

        public async Task<IEnumerable<RestPostComment>> GetCommentsAsync(TwitchPageOptions options = null)
            => await PostHelper.GetCommentsAsync(this, options);
        
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
