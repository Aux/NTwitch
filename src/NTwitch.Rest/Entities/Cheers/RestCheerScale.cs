using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.CheerScale;

namespace NTwitch.Rest
{
    public class RestCheerScale
    {
        public BaseRestClient Client { get; }
        public IReadOnlyDictionary<double, string> Animated { get; private set; }
        public IReadOnlyDictionary<double, string> Static { get; private set; }

        internal RestCheerScale(BaseRestClient client, Model model)
        {
            Client = client;
            Update(model);
        }
        
        internal virtual void Update(Model model)
        {
            Animated = model.Animated;
            Static = model.Static;
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
