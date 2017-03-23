using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.CheerInfo;

namespace NTwitch.Rest
{
    public class RestCheerInfo
    {
        public BaseRestClient Client { get; }
        public string Prefix { get; private set; }
        public IReadOnlyCollection<string> Backgrounds { get; private set; }
        public IReadOnlyCollection<double> Scales { get; private set; }
        public IReadOnlyCollection<string> States { get; private set; }
        public IReadOnlyCollection<RestCheer> Tiers { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string Type { get; private set; }

        internal RestCheerInfo(BaseRestClient client, Model model)
        {
            Client = client;
            Update(model);
        }
        
        internal virtual void Update(Model model)
        {
            Prefix = model.Prefix;
            Backgrounds = model.Backgrounds.ToArray();
            Scales = model.Scales.ToArray();
            States = model.States.ToArray();
            UpdatedAt = model.UpdatedAt;
            Type = model.Type;
            Tiers = model.Tiers.Select(x =>
            {
                var cheer = new RestCheer(Client, x.Id);
                cheer.Update(x);
                return cheer;
            }).ToArray();
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
