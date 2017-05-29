using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Team;

namespace NTwitch.Rest
{
    public class RestTeam : RestSimpleTeam, IUpdateable
    {
        /// <summary> All channels associated with this team </summary>
        public IReadOnlyCollection<RestChannel> Channels { get; private set; }

        internal RestTeam(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal new static RestTeam Create(BaseRestClient client, Model model)
        {
            var entity = new RestTeam(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            Channels = model.Channels.Select(x =>
            {
                var entity = new RestChannel(Client, x.Id);
                entity.Update(x);
                return entity;
            }).ToArray();
        }

        /// <summary> Get the most recent information for this entity </summary>
        public virtual async Task UpdateAsync()
        {
            var token = TokenHelper.GetSingleToken(Client);
            var model = await Client.RestClient.GetTeamInternalAsync(token, Name);
            Update(model);
        }
    }
}
