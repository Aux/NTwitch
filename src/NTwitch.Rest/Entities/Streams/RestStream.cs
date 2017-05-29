using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Stream;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity<ulong>, IUpdateable, IEqualityComparer<RestStream>
    {
        /// <summary> Date and time when this stream was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The name of the game being streamed </summary>
        public string Game { get; private set; }
        /// <summary> The delay in seconds of this stream </summary>
        public int Delay { get; private set; }
        /// <summary> The number of viewers currently watching this stream </summary>
        public uint Viewers { get; private set; }
        /// <summary> The height of this stream's video </summary>
        public uint VideoHeight { get; private set; }
        /// <summary> The average fps of this stream's video </summary>
        public double AverageFps { get; private set; }
        /// <summary> True if this stream is pre-recorded </summary>
        public bool IsPlaylist { get; private set; }
        /// <summary> Preview images for this stream </summary>
        public IReadOnlyDictionary<string, string> Previews { get; private set; }
        /// <summary> The channel this stream is associated with </summary>
        public RestChannel Channel { get; private set; }

        internal RestStream(BaseRestClient client, ulong id)
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

        public bool Equals(RestStream x, RestStream y)
            => x.Id == y.Id;
        public int GetHashCode(RestStream obj)
            => obj.GetHashCode();

        /// <summary> Get the most recent information for this entity </summary>
        public virtual async Task UpdateAsync()
        {
            var token = TokenHelper.GetSingleToken(Client);
            var model = await Client.RestClient.GetStreamInternalAsync(token, Id, StreamType.All);
            Update(model.Stream);
        }
    }
}
