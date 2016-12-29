using System.Collections.Generic;

namespace NTwitch
{
    public interface ITeam : IEntity, ITeamSummary
    {
        IEnumerable<IUser> Users { get; }
    }
}
