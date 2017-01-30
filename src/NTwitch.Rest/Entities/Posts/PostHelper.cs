using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class PostHelper
    {
        public static Task<RestPostComment> CreateCommentAsync(RestPost post, BaseRestClient client, string content)
        {
            throw new NotImplementedException();
        }

        public static Task CreateReactionAsync(RestPost post, BaseRestClient client, uint emoteid)
        {
            throw new NotImplementedException();
        }

        public static Task DeleteAsync(RestPost post, BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task DeleteCommentAsync(RestPost post, BaseRestClient client, uint commentid)
        {
            throw new NotImplementedException();
        }

        public static Task DeleteReactionAsync(RestPost post, BaseRestClient client, uint emoteid)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestPostComment>> GetCommentsAsync(RestPost post, BaseRestClient client, PageOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
