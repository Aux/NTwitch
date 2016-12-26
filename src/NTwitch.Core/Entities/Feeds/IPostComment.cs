using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IPostComment : IEntity
    {
        string Body { get; set; }
        DateTime CreatedAt { get; set; }
        bool IsDeleted { get; set; }
        IEnumerable<IEmote> Emotes { get; set; }
        IEnumerable<IPostReaction> Reactions { get; set; }
        IUser User { get; set; }
    }
}
