using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Stream;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity<ulong>
    {
        public DateTime CreatedAt { get; private set; }
        public string Game { get; private set; }
        public int Delay { get; private set; }
        public uint Viewers { get; private set; }
        public uint VideoHeight { get; private set; }
        public double AverageFps { get; private set; }
        public bool IsPlaylist { get; private set; }
        public IReadOnlyDictionary<string, string> Previews { get; private set; }
        public RestChannel Channel { get; private set; }

        public RestStream(BaseRestClient client, ulong id)
            : base(client, id) { }

        internal static RestStream Create(BaseRestClient client, Model model)
        {
            var entity = new RestStream(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Channel = new RestChannel(Client, model.Channel.Id);
            Channel.Update(model.Channel);

            CreatedAt = model.CreatedAt;
            Game = model.Game;
            Delay = model.Delay;
            Viewers = model.Viewers;
            VideoHeight = model.VideoHeight;
            AverageFps = model.AverageFps;
            IsPlaylist = model.IsPlaylist;
            Previews = model.Previews;
        }

        //public virtual async Task UpdateAsync()
        //{
        //    var entity = await Client.RestClient.GetStreamAsync().ConfigureAwait(false);
        //    Update(entity);
        //}
    }
}
