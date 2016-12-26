using System.Collections.Generic;

namespace NTwitch
{
    public interface IEmote : IEntity
    {
        string Name { get; set; }
        IEnumerable<IEmoteImage> Images { get; set; }
    }
}
