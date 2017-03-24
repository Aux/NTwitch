using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestTopCommunity : RestSimpleCommunity
    {
        public uint Channels { get; private set; }
        public uint Viewers { get; private set; }

        public RestTopCommunity(BaseRestClient client, string id) 
            : base(client, id) { }
        
        internal new static RestTopCommunity Create(BaseRestClient client, Model model)
        {
            var entity = new RestTopCommunity(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            Channels = model.Channels;
            Viewers = model.Viewers;
        }
    }
}
