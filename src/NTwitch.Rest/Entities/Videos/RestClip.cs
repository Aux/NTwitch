using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Clip;

namespace NTwitch.Rest
{
    public class RestClip : RestEntity<string>, IClip
    {
        /// <summary> The date and time this clip was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The user whose channel this clip was created on </summary>
        public RestSimpleUser Broadcaster { get; private set; }
        /// <summary> The user who created this clip </summary>
        public RestSimpleUser Curator { get; private set; }
        /// <summary> The vod associated with this clip </summary>
        public RestSimpleVideo Vod { get; private set; }
        /// <summary> The code to embed this clip on a website </summary>
        public string EmbedHtml { get; private set; }
        /// <summary> The url used to embed this clip </summary>
        public string EmbedUrl { get; private set; }
        /// <summary> The game being played in this clip </summary>
        public string Game { get; private set; }
        /// <summary> The language used in this clip </summary>
        public string Language { get; private set; }
        /// <summary> The title of this clip </summary>
        public string Title { get; private set; }
        /// <summary> The url to this clip </summary>
        public string Url { get; private set; }
        /// <summary> Preview images for this clip </summary>
        public IReadOnlyDictionary<string, string> Thumbnails { get; private set; }
        /// <summary>  </summary>
        public ulong TrackingId { get; private set; }
        /// <summary>  </summary>
        public double Duration { get; private set; }
        /// <summary> The total number of views this clip has received </summary>
        public uint Views { get; private set; }

        internal RestClip(TwitchRestClient client, string id)
            : base(client, id) { }

        internal static RestClip Create(TwitchRestClient client, Model model)
        {
            var entity = new RestClip(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Broadcaster = RestSimpleUser.Create(Client, model.Broadcaster);
            Curator = RestSimpleUser.Create(Client, model.Curator);
            Vod = RestSimpleVideo.Create(Client, model.Vod);
            CreatedAt = model.CreatedAt;
            EmbedHtml = model.EmbedHtml;
            EmbedUrl = model.EmbedUrl;
            Game = model.Game;
            Language = model.Language;
            Title = model.Title;
            Url = model.Url;
            Thumbnails = model.Thumbnails;
            TrackingId = model.TrackingId;
            Duration = model.Duration;
            Views = model.Views;
        }

        // IEqualityComparer
        public bool Equals(IClip x, IClip y) => x.Id == y.Id;
        public int GetHashCode(IClip obj) => obj.GetHashCode();

        /// <summary> Get the most recent information for this entity </summary>
        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
