using System.Collections.Generic;

namespace NTwitch
{
    public interface IEmote : IEntity<ulong>, IEqualityComparer<IEmote>
    {
    }
}
