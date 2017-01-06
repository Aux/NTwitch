using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal class PostHelper
    {
        public static Task CreateReactionAsync(RestPostComment post, ulong emoteid)
        {
            throw new NotImplementedException();
        }

        public static Task DeleteReactionAsync(RestPostComment post, ulong emoteid)
        {
            throw new NotImplementedException();
        }

        internal static Task<RestPostComment> CreateCommentAsync(RestPost restPost, string content)
        {
            throw new NotImplementedException();
        }

        internal static Task<RestPost> DeleteAsync(RestPost restPost)
        {
            throw new NotImplementedException();
        }

        internal static Task<RestPostComment> DeleteCommentAsync(RestPost restPost, ulong commentid)
        {
            throw new NotImplementedException();
        }

        internal static Task<IEnumerable<RestPostComment>> GetCommentsAsync(RestPost restPost, TwitchPageOptions options)
        {
            throw new NotImplementedException();
        }

        internal static Task<RestPostComment> DeleteReactionAsync(RestPost restPost, ulong emoteid)
        {
            throw new NotImplementedException();
        }

        internal static Task<Task> CreateReactionAsync(RestPost restPost, ulong emoteid)
        {
            throw new NotImplementedException();
        }
    }
}
