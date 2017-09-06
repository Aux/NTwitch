using Model = NTwitch.Rest.API.Follow;

namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow
    {
        /// <summary> The channel associated with this follow </summary>
        public RestChannel Channel { get; private set; }

        internal RestChannelFollow(BaseTwitchClient client) 
            : base(client) { }

        internal new static RestChannelFollow Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestChannelFollow(client);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            Channel = RestChannel.Create(Client, model.Channel);
        }
    }
}
