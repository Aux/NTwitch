using Model = NTwitch.Rest.API.Follow;

namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow, IChannelFollow
    {
        /// <summary> The channel associated with this follow </summary>
        public RestChannel Channel { get; private set; }

        internal RestChannelFollow(TwitchRestClient client) 
            : base(client) { }

        internal new static RestChannelFollow Create(TwitchRestClient client, Model model)
        {
            var entity = new RestChannelFollow(client);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            Channel = new RestChannel(Client, model.Channel.Id);
            Channel.Update(model.Channel);
            base.Update(model);
        }
    }
}
