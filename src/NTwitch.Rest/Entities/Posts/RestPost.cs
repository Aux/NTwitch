using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestPost : RestEntity
    {
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

        internal RestPost(BaseRestClient client) : base(client) { }

        internal static RestPost Create(BaseRestClient client, string json)
        {
            var post = new RestPost(client);
            JsonConvert.PopulateObject(json, post);
            return post;
        }
        
        public Task<RestPostComment> CreateCommentAsync(string content)
            => PostHelper.CreateCommentAsync(this, Client, content);
        public Task CreateReactionAsync(ulong emoteid)
            => PostHelper.CreateReactionAsync(this, Client, emoteid);
        public Task DeleteAsync()
            => PostHelper.DeleteAsync(this, Client);
        public Task DeleteCommentAsync(ulong commentid)
            => PostHelper.DeleteCommentAsync(this, Client, commentid);
        public Task DeleteReactionAsync(ulong emoteid)
            => PostHelper.DeleteReactionAsync(this, Client, emoteid);
        public Task<IEnumerable<RestPostComment>> GetCommentsAsync(PageOptions options = null)
            => PostHelper.GetCommentsAsync(this, Client, options);
    }
}
