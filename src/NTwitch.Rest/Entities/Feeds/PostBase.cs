using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class PostBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal PostBase(BaseRestClient client)
        {
            Client = client;
        }
        
        public Task<RestPostComment> CreateCommentAsync(string content)
            => PostHelper.CreateCommentAsync(this, content);

        public Task CreateReactionAsync(ulong emoteid)
            => PostHelper.CreateReactionAsync(this, emoteid);

        public Task<RestPost> DeleteAsync()
            => PostHelper.DeleteAsync(this);

        public Task<RestPostComment> DeleteCommentAsync(ulong commentid)
            => PostHelper.DeleteCommentAsync(this, commentid);

        public Task<RestPostComment> DeleteReactionAsync(ulong emoteid)
            => PostHelper.DeleteReactionAsync(this, emoteid);

        public Task<IEnumerable<RestPostComment>> GetCommentsAsync(PageOptions options = null)
            => PostHelper.GetCommentsAsync(this, options);
    }
}
