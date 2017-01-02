using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestTeam : RestTeamSummary, ITeam
    {
        public IEnumerable<RestUser> Users { get; internal set; }

        public RestTeam(ITwitchClient client) : base(client) { }

        //ITeam
        IEnumerable<IUser> ITeam.Users
            => Users;
    }
}
