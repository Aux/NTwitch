using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Cheer;

namespace NTwitch.Rest
{
    public class RestCheer : RestEntity<ulong>
    {
        public string Color { get; private set; }
        public int MinimumBits { get; private set; }
        public IEnumerable<RestCheerImage> Images { get; private set; }

        internal RestCheer(BaseRestClient client, ulong id)
            : base(client, id) { }

        internal static RestCheer Create(BaseRestClient client, Model model)
        {
            var entity = new RestCheer(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Color = model.Color;
            MinimumBits = model.MinimumBits;
            Images = model.Images.Select(x => new RestCheerImage(Client, x));
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
