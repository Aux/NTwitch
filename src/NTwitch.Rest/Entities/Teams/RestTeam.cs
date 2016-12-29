using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestTeam : RestTeamSummary, ITeam
    {
        public IEnumerable<RestUser> Users { get; }

        public RestTeam(TwitchRestClient client, ulong id) : base(client, id) { }

        //ITeam
        IEnumerable<IUser> ITeam.Users
            => Users;
    }
}
