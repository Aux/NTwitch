using System.Collections.Generic;

namespace NTwitch
{
    public interface IClip : IEntity<string>, IUpdateable, IEqualityComparer<IClip>
    {
    }
}
