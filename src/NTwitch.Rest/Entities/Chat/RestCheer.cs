﻿using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Rest.API.Cheer;

namespace NTwitch.Rest
{
    public class RestCheer : RestEntity<ulong>, ICheer
    {
        /// <summary> The hexadecimal color of this cheer </summary>
        public string Color { get; private set; }
        /// <summary> The minimum number of bits required to use this cheer </summary>
        public int MinimumBits { get; private set; }
        /// <summary> The images that appear when this cheer is posted in chat </summary>
        public IReadOnlyCollection<RestCheerImage> Images { get; private set; }

        internal RestCheer(BaseTwitchClient client, ulong id)
            : base(client, id) { }

        internal static RestCheer Create(BaseTwitchClient client, Model model)
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

        // IEqualityComparer
        public bool Equals(ICheer x, ICheer y) => x.Id == y.Id;
        public int GetHashCode(ICheer obj) => obj.GetHashCode();
    }
}
