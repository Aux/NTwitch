using System;
using System.Collections.Generic;
using Model = NTwitch.Rest.API.Game;

namespace NTwitch.Rest
{
    public class RestGame : RestEntity<ulong>, IEquatable<RestGame>
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

        internal RestGame(BaseTwitchClient client, ulong id) 
            : base(client, id) { }
        
        public bool Equals(RestGame other)
            => Id == other.Id;
        public override string ToString()
            => Name;

        internal static RestGame Create(BaseTwitchClient client, Model model)
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
    }
}
