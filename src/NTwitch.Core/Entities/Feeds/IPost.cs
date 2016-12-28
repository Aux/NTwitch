using System;
using System.Collections.Generic;

namespace NTwitch
{
    public interface IPost : IEntity
    {
        string Body { get; }
        DateTime CreatedAt { get; }
        bool IsDeleted { get; }
        IEnumerable<IEmbed> Embeds { get; }
        IEnumerable<IEmote> Emotes { get; }
        IPostPermissions Permissions { get; }
        IEnumerable<IPostReaction> Reactions { get; }
        IUser User { get; set; }
    }
}
