using Model = NTwitch.Rest.API.FeaturedStream;

namespace NTwitch.Rest
{
    public class RestFeaturedStream
    {
        /// <summary> The stream that is being featured </summary>
        public RestStream Stream { get; private set; }
        /// <summary> The image shown to preview this stream </summary>
        public string Image { get; private set; }
        /// <summary> The level of priority this stream gets on the front page of twitch </summary>
        public int Priority { get; private set; }
        /// <summary> True if this stream is scheduled </summary>
        public bool Scheduled { get; private set; }
        /// <summary> True if this stream is sponsored </summary>
        public bool Sponsored { get; private set; }
        /// <summary> The text description of this featured stream </summary>
        public string Text { get; private set; }
        /// <summary> The title of this featured stream </summary>
        public string Title { get; private set; }
        
        internal static RestFeaturedStream Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestFeaturedStream();
            entity.Update(client, model);
            return entity;
        }

        internal virtual void Update(BaseTwitchClient client, Model model)
        {
            Stream = new RestStream(client, model.Stream.Id);
            Stream.Update(model.Stream);

            Image = model.Image;
            Priority = model.Priority;
            Scheduled = model.Scheduled;
            Sponsored = model.Sponsored;
            Text = model.Text;
            Title = model.Title;
        }
    }
}
