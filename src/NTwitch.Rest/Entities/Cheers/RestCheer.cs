using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Rest.API.Cheer;

namespace NTwitch.Rest
{
    public class RestCheer : RestEntity<ulong>, IEqualityComparer<RestCheer>
    {
        /// <summary> The hexadecimal color of this cheer </summary>
        public string Color { get; private set; }
        /// <summary> The minimum number of bits required to use this cheer </summary>
        public int MinimumBits { get; private set; }
        /// <summary> The images that appear when this cheer is posted in chat </summary>
        public IReadOnlyCollection<RestCheerImage> Images { get; private set; }

        internal RestCheer(TwitchRestClient client, ulong id)
            : base(client, id) { }

        internal static RestCheer Create(TwitchRestClient client, Model model)
        {
            var entity = new RestCheer(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Color = model.Color;
            MinimumBits = model.MinimumBits;
            Images = model.Images.Select(x => new RestCheerImage(Client, x)).ToArray();
        }

        public bool Equals(RestCheer x, RestCheer y)
            => x.Id == y.Id;
        public int GetHashCode(RestCheer obj)
            => obj.GetHashCode();
    }
}
