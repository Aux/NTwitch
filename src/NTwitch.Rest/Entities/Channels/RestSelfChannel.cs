using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestSelfChannel : RestChannel, ISelfChannel, IUpdateable
    {
        /// <summary> The email associated with this channel </summary>
        public string Email { get; private set; }
        /// <summary> The key used to stream video to twitch as this channel </summary>
        public string StreamKey { get; private set; }

        internal RestSelfChannel(BaseTwitchClient client, ulong id, string name) 
            : base(client, id, name) { }

        internal new static RestSelfChannel Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestSelfChannel(client, model.Id, model.Name);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            Email = model?.Email ?? Email;
            StreamKey = model.StreamKey;
            base.Update(model);
        }

        /// <summary> Get the most recent information for this entity </summary>
        public override async Task UpdateAsync()
        {
            var model = await Client.ApiClient.GetMyChannelAsync(null).ConfigureAwait(false);
            Update(model);
        }
    }
}
