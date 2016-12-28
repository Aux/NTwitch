using System.Collections.Generic;

namespace NTwitch
{
    public interface IEmote : IEntity
    {
        string Name { get; }
        IEnumerable<IEmoteImage> Images { get; }
    }
}
