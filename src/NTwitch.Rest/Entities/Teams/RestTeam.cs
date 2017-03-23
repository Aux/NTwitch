using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Rest.API.Team;

namespace NTwitch.Rest
{
    public class RestTeam : RestSimpleTeam
    {
        public IReadOnlyCollection<RestChannel> Channels { get; private set; }

        public RestTeam(BaseRestClient client, ulong id) 
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
    }
}
