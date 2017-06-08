using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Video;

namespace NTwitch.Rest
{
    public class RestVideo : RestSimpleVideo, IVideo, IUpdateable
    {
        /// <summary> The title of this video </summary>
        public string Title { get; private set; }
        /// <summary> The description provided for this video </summary>
        public string Description { get; private set; }
        /// <summary> The raw html description provided for this video </summary>
        public string DescriptionHtml { get; private set; }
        /// <summary> The id of the broadcast associated with this video </summary>
        public uint BroadcastId { get; private set; }
        /// <summary>  </summary>
        public string BroadcastType { get; private set; }
        /// <summary>  </summary>
        public string Status { get; private set; }
        /// <summary> The tags associated with this video </summary>
        public string[] Tags { get; private set; }
        /// <summary> The language used in this video </summary>
        public string Language { get; private set; }
        /// <summary> The game displayed in this video </summary>
        public string Game { get; private set; }
        /// <summary> The length in seconds of this video </summary>
        public uint Length { get; private set; }
        /// <summary> The total number of views this video has received </summary>
        public uint Views { get; private set; }
        /// <summary>  </summary>
        public string Viewable { get; private set; }
        /// <summary> The date and time this video will become viewable </summary>
        public DateTime? ViewableAt { get; private set; }
        /// <summary> The date and time this video was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The date and time this video was published </summary>
        public DateTime PublishedAt { get; private set; }
        /// <summary> The date and time this video was recorded </summary>
        public DateTime RecordedAt { get; private set; }
        /// <summary> The url of this video's animated preview </summary>
        public string AnimatedPreviewUrl { get; private set; }
        /// <summary> Available qualities for this video </summary>
        public IReadOnlyDictionary<string, float> Qualities { get; private set; }
        /// <summary> Available resolutions for this video </summary>
        public IReadOnlyDictionary<string, string> Resolutions { get; private set; }
        /// <summary> The channel associated with this video </summary>
        public RestSimpleChannel Channel { get; private set; }
        
        ///// <summary>  </summary>
        //public PreviewImage Preview { get; private set; }
        ///// <summary>  </summary>
        //public VideoThumbnail Thumbnail { get; private set; }
        
        internal RestVideo(TwitchRestClient client, string id) 
            : base(client, id) { }

        internal new static RestVideo Create(TwitchRestClient client, Model model)
        {
            var entity = new RestVideo(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);

            Title = model.Title;
            Description = model.Description;
            DescriptionHtml = model.Description;
            BroadcastId = model.BroadcastId;
            BroadcastType = model.BroadcastType;
            Status = model.Status;
            Tags = model.Tags.Split(' ');
            Language = model.Language;
            Game = model.Game;
            Length = model.Length;
            Views = model.Views;
            Viewable = model.Viewable;
            ViewableAt = model.ViewableAt;
            CreatedAt = model.CreatedAt;
            PublishedAt = model.PublishedAt;
            RecordedAt = model.RecordedAt;
            AnimatedPreviewUrl = model.AnimatedPreviewUrl;
            Qualities = model.Fps;
            Resolutions = model.Resolutions;

            Channel = RestChannel.Create(Client, model.Channel);

            //Preview = model.Preview;
            //Thumbnail = model.Thumbnails;
        }

        /// <summary> Get the most recent information for this entity </summary>
        public virtual async Task UpdateAsync()
        {
            var model = await Client.ApiClient.GetVideoAsync(Id, null).ConfigureAwait(false);
            Update(model);
        }
        
        /// <summary> Modify the properties of this video </summary>
        public Task ModifyAsync(Action<ModifyVideoParams> changes, RequestOptions options = null)
            => VideoHelper.ModifyAsync(this, changes, options);
        /// <summary> Delete this video </summary>
        public Task DeleteAsync(RequestOptions options = null)
            => VideoHelper.DeleteAsync(this, options);
    }
}
