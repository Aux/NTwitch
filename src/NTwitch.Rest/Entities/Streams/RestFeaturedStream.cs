using Model = NTwitch.Rest.API.FeaturedStream;

namespace NTwitch.Rest
{
    public class RestFeaturedStream
    {
        public RestStream Stream { get; private set; }
        public string Image { get; private set; }
        public int Priority { get; private set; }
        public bool Scheduled { get; private set; }
        public bool Sponsored { get; private set; }
        public string Text { get; private set; }
        public string Title { get; private set; }
        
        internal static RestFeaturedStream Create(BaseRestClient client, Model model)
        {
            var entity = new RestFeaturedStream();
            entity.Update(client, model);
            return entity;
        }

        internal virtual void Update(BaseRestClient client, Model model)
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
