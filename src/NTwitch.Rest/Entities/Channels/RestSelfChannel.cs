using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestSelfChannel : RestChannel
    {
        /// <summary> The email associated with this channel </summary>
        public string Email { get; private set; }
        /// <summary> The key used to stream video to twitch as this channel </summary>
        public string StreamKey { get; private set; }

        internal RestSelfChannel(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal new static RestSelfChannel Create(BaseRestClient client, Model model)
        {
            var entity = new RestSelfChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            Email = model?.Email ?? Email;
            StreamKey = model.StreamKey;
            base.Update(model);
        }

        /// <summary> Update this channel's properties </summary>
        public override async Task UpdateAsync()
        {
            var entity = await Client.RestClient.GetCurrentChannelAsync().ConfigureAwait(false);
            Update(entity);
        }
    }
}
