using System.Collections.Generic;

namespace NTwitch
{
    public interface IEmoteSet : IEntity
    {
        IEnumerable<IEmote> Emotes { get; }
    }
}
