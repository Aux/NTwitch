using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Clip;

namespace NTwitch.Rest
{
    public class RestClip : RestEntity<string>
    {
        public DateTime CreatedAt { get; set; }
        public RestSimpleUser Broadcaster { get; set; }
        public RestSimpleUser Curator { get; set; }
        public RestSimpleVideo Vod { get; set; }
        public string EmbedHtml { get; set; }
        public string EmbedUrl { get; set; }
        public string Game { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public IReadOnlyDictionary<string, string> Thumbnails { get; set; }
        public ulong TrackingId { get; set; }
        public double Duration { get; set; }
        public uint Views { get; set; }

        public RestClip(BaseRestClient client, string id)
            : base(client, id) { }

        internal static RestClip Create(BaseRestClient client, Model model)
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

        public async Task UpdateAsync()
        {
            var token = TokenHelper.GetSingleToken(Client);
            var model = await Client.RestClient.GetClipInternalAsync(token, Id);
            this.Update(model);
        }
    }
}
