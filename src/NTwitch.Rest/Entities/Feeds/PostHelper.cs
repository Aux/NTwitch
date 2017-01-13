using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class PostHelper
    {
        public static Task<RestPostComment> CreateCommentAsync(PostBase postBase, string content)
        {
            throw new NotImplementedException();
        }

        public static Task CreateReactionAsync(PostBase postBase, ulong emoteid)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> DeleteAsync(PostBase postBase)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPostComment> DeleteCommentAsync(PostBase postBase, ulong commentid)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestPostComment>> GetCommentsAsync(PostBase postBase, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPostComment> DeleteReactionAsync(PostBase postBase, ulong emoteid)
        {
            throw new NotImplementedException();
        }
    }
}
