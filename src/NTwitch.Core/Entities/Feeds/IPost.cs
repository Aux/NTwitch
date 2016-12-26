using System;
using System.Collections.Generic;

namespace NTwitch
{
    public interface IPost : IEntity
    {
        string Body { get; set; }
        DateTime CreatedAt { get; set; }
        bool IsDeleted { get; set; }
        IEnumerable<IEmbed> Embeds { get; set; }
        IEnumerable<IEmote> Emotes { get; set; }
        IPostPermissions Permissions { get; set; }
        IEnumerable<IPostReaction> Reactions { get; set; }
        IUser User { get; set; }
    }
}
