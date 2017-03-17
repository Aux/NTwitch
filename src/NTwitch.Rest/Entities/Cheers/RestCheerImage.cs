using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.CheerImage;

namespace NTwitch.Rest
{
    public class RestCheerImage
    {
        public BaseRestClient Client { get; }
        public RestCheerScale Dark { get; private set; }
        public RestCheerScale Light { get; private set; }

        internal RestCheerImage(BaseRestClient client, Model model)
        {
            Client = client;
        }
        
        internal virtual void Update(Model model)
        {
            Dark = new RestCheerScale(Client, model.Dark);
            Light = new RestCheerScale(Client, model.Light);
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
