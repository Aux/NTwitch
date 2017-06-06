using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestTopCommunity : RestSimpleCommunity
    {
        /// <summary> The number of channels in this community </summary>
        public uint Channels { get; private set; }
        /// <summary> The total number of viewers watching channels in this community </summary>
        public uint Viewers { get; private set; }

        internal RestTopCommunity(TwitchRestClient client, string id) 
            : base(client, id) { }
        
        internal new static RestTopCommunity Create(TwitchRestClient client, Model model)
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
