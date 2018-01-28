using Model = NTwitch.Rest.API.FeaturedBroadcast;

namespace NTwitch.Rest
{
    public class RestFeaturedBroadcast
    {
        /// <summary> The stream that is being featured </summary>
        public RestBroadcast Stream { get; private set; }
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
        
        internal static RestFeaturedBroadcast Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestFeaturedBroadcast();
            entity.Update(client, model);
            return entity;
        }

        internal virtual void Update(BaseTwitchClient client, Model model)
        {
            Stream = new RestBroadcast(client, model.Stream.Id);
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
