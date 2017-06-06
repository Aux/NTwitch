using System;
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

        internal RestSelfChannel(TwitchRestClient client, ulong id) 
            : base(client, id) { }

        internal new static RestSelfChannel Create(TwitchRestClient client, Model model)
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

        /// <summary> Get the most recent information for this entity </summary>
        public override Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
