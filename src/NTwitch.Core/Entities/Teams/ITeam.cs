using System.Collections.Generic;

namespace NTwitch
{
    public interface ITeam : IEntity, ITeamInfo
    {
        IEnumerable<IUser> Users { get; }
    }
}
