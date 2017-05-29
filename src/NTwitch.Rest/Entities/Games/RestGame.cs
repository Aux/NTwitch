using System;
using System.Collections.Generic;
using Model = NTwitch.Rest.API.Game;

namespace NTwitch.Rest
{
    public class RestGame : RestEntity<ulong>, IEqualityComparer<RestGame>
    {
        /// <summary> The name of this game </summary>
        public string Name { get; private set; }
        /// <summary> The total number of viewers for this game </summary>
        public uint Viewers { get; private set; }
        /// <summary> This game's giantbomb id </summary>
        public ulong GiantbombId { get; private set; }
        /// <summary> The urls for this game's box art images </summary>
        public IReadOnlyDictionary<string, string> Box { get; private set; }
        /// <summary> The urls for this game's logo images </summary>
        public IReadOnlyDictionary<string, string> Logo { get; private set; }

        internal RestGame(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestGame Create(BaseRestClient client, Model model)
        {
            var entity = new RestGame(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Name = model.Name;
            Viewers = model.Viewers;
            GiantbombId = model.GiantbombId;
            Box = model.Box;
            Logo = model.Logo;
        }

        public bool Equals(RestGame x, RestGame y)
            => x.Id == y.Id;
        public int GetHashCode(RestGame obj)
            => obj.GetHashCode();
    }
}
