using System.Collections.Generic;

namespace NTwitch
{
    public interface ICheer : IEntity<ulong>, IEqualityComparer<ICheer>
    {
    }
}
