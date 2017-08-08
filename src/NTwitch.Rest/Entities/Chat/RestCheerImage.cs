using Model = NTwitch.Rest.API.CheerImage;

namespace NTwitch.Rest
{
    public class RestCheerImage
    {
        /// <summary> An instance of the client that created this entity </summary>
        public BaseTwitchClient Client { get; }
        /// <summary> The dark theme version of this cheer </summary>
        public RestCheerScale Dark { get; private set; }
        /// <summary> The light theme version of this cheer </summary>
        public RestCheerScale Light { get; private set; }

        internal RestCheerImage(BaseTwitchClient client)
        {
            Client = client;
        }
        
        internal static RestCheerImage Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestCheerImage(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Dark = RestCheerScale.Create(Client, model.Dark);
            Light = RestCheerScale.Create(Client, model.Light);
        }
    }
}
