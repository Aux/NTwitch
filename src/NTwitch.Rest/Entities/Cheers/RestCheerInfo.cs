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
        public IEnumerable<string> Backgrounds { get; private set; }
        public IEnumerable<double> Scales { get; private set; }
        public IEnumerable<string> States { get; private set; }
        public IEnumerable<RestCheer> Tiers { get; private set; }
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
            Backgrounds = model.Backgrounds;
            Scales = model.Scales;
            States = model.States;
            UpdatedAt = model.UpdatedAt;
            Type = model.Type;
            Tiers = model.Tiers.Select(x =>
            {
                var cheer = new RestCheer(Client, x.Id);
                cheer.Update(x);
                return cheer;
            });
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
