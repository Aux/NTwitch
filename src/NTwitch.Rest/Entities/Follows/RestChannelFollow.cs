using Model = NTwitch.Rest.API.ChannelFollow;

namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow
    {
        public RestChannel Channel { get; private set; }

        internal RestChannelFollow(BaseRestClient client) 
            : base(client) { }

        internal static RestChannelFollow Create(BaseRestClient client, Model model)
        {
            var entity = new RestChannelFollow(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Channel = new RestChannel(Client, model.Channel.Id);
            Channel.Update(model.Channel);
            base.Update(model);
        }
    }
}
