using System.Collections.Generic;

namespace NTwitch
{
    public interface ISimpleUser : IEntity<ulong>, IEqualityComparer<ISimpleUser>
    {
    }
}
