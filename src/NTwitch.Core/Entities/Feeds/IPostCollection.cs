using System.Collections.Generic;

namespace NTwitch
{
    public interface IPostCollection
    {
        ulong Cursor { get; set; }
        int Count { get; set; }
        string Topic { get; set; }
        IEnumerable<IPost> Posts { get; set; }
    }
}
