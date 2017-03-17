using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.SimpleChannel;

namespace NTwitch.Rest
{
    public class RestSimpleChannel : RestEntity<ulong>, IChannel
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        internal RestSimpleChannel(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleChannel Create(BaseRestClient client, Model model)
        {
            var entity = new RestSimpleChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            DisplayName = model.DisplayName;
            Name = model.Name;
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        // Cheers
        public Task<IEnumerable<RestCheerInfo>> GetCheersAsync()
            => ClientHelper.GetCheersAsync(Client, Id);
    }
}
