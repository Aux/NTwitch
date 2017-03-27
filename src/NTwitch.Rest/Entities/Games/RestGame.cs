using System.Collections.Generic;
using Model = NTwitch.Rest.API.Game;

namespace NTwitch.Rest
{
    public class RestGame : RestEntity<ulong>
    {
        public string Name { get; private set; }
        public uint Viewers { get; private set; }
        public ulong GiantbombId { get; private set; }
        public IReadOnlyDictionary<string, string> Box { get; private set; }
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
    }
}
