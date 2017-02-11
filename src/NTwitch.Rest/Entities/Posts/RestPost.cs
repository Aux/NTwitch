using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestPost : RestEntity<string>
    {
        [JsonProperty("body")]
        public string Body { get; internal set; }
        [JsonProperty("comments")]
        public IEnumerable<RestPostComment> Comments { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("emotes")]
        public IEnumerable<RestPostEmote> Emotes { get; internal set; }
        [JsonProperty("deleted")]
        public bool IsDeleted { get; internal set; }
        [JsonProperty("permissions")]
        public RestPostPermissions Permissions { get; internal set; }
        [JsonProperty("reactions")]
        public IEnumerable<RestPostReaction> Reactions { get; internal set; }
        [JsonProperty("user")]
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
        public Task CreateReactionAsync(uint emoteid)
            => PostHelper.CreateReactionAsync(this, Client, emoteid);
        public Task DeleteAsync()
            => PostHelper.DeleteAsync(this, Client);
        public Task DeleteCommentAsync(uint commentid)
            => PostHelper.DeleteCommentAsync(this, Client, commentid);
        public Task DeleteReactionAsync(uint emoteid)
            => PostHelper.DeleteReactionAsync(this, Client, emoteid);
        public Task<IEnumerable<RestPostComment>> GetCommentsAsync()
            => GetCommentsAsync(null);
        public Task<IEnumerable<RestPostComment>> GetCommentsAsync(PageOptions options = null)
            => PostHelper.GetCommentsAsync(this, Client, options);
    }
}
