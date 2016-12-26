using System.Collections.Generic;

namespace NTwitch
{
    public interface IPostCommentCollection
    {
        ulong Cursor { get; set; }
        int Count { get; set; }
        IEnumerable<IPostComment> Comments { get; set; }
    }
}
