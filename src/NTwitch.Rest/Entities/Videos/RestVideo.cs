using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Video;

namespace NTwitch.Rest
{
    public class RestVideo : RestEntity<string>
    {
        public RestSimpleChannel Channel { get; private set; }
        public DateTime? ViewableAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime PublishedAt { get; private set; }
        public string[] Tags { get; private set; }
        public string Viewable { get; private set; }
        public string BroadcastType { get; private set; }
        public string Description { get; private set; }
        public string DescriptionHtml { get; private set; }
        public string Game { get; private set; }
        public string Language { get; private set; }
        public string Status { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public uint BroadcastId { get; private set; }
        public uint Length { get; private set; }
        public uint Views { get; private set; }
        public IEnumerable<VideoSegment> MutedSegments { get; private set; }
        public Dictionary<string, float> Qualities { get; private set; }
        public Dictionary<string, string> Previews { get; private set; }
        public Dictionary<string, string> Resolutions { get; private set; }
        public Dictionary<string, VideoThumbnail> Thumbnails { get; private set; }

        internal RestVideo(BaseRestClient client, string id) 
            : base(client, id) { }

        internal static RestVideo Create(BaseRestClient client, Model model)
        {
            var entity = new RestVideo(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Channel = new RestSimpleChannel(Client, model.Channel.Id);
            Channel.Update(model.Channel);
            BroadcastId = model.BroadcastId;
            BroadcastType = model.BroadcastType;
            CreatedAt = model.CreatedAt;
            Description = model.Description;
            DescriptionHtml = model.Description;
            Qualities = model.Fps;
            Game = model.Game;
            Language = model.Language;
            Length = model.Length;
            MutedSegments = model.MutedSegments;
            Previews = model.PreviewImages;
            PublishedAt = model.PublishedAt;
            Resolutions = model.Resolutions;
            Status = model.Status;
            Tags = model.Tags.Split(' ');
            Thumbnails = model.Thumbnails;
            Title = model.Title;
            Url = model.Url;
            Viewable = model.Viewable;
            ViewableAt = model.ViewableAt;
            Views = model.Views;
        }

        public async Task UpdateAsync()
        {
            var entity = await Client.RestClient.GetVideoAsync(Id).ConfigureAwait(false);
            Update(entity);
        }
    }
}
