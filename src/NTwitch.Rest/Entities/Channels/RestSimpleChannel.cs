using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestSimpleChannel : RestEntity<ulong>, IChannel, IEqualityComparer<IChannel>
    {
        /// <summary> This channel's internal twitch username </summary>
        public string Name { get; private set; }
        /// <summary> This channel's display username </summary>
        public string DisplayName { get; private set; }

        internal RestSimpleChannel(TwitchRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleChannel Create(TwitchRestClient client, Model model)
        {
            var entity = new RestSimpleChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            DisplayName = model.DisplayName;
            Name = model.Name;
        }

        public bool Equals(IChannel x, IChannel y)
            => x.Id == y.Id;
        public int GetHashCode(IChannel obj)
            => obj.GetHashCode();
    }
}
