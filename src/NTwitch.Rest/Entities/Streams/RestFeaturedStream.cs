namespace NTwitch.Rest
{
    public class RestFeaturedStream : RestEntity, IFeaturedStream
    {
        public string ImageUrl { get; }
        public bool IsScheduled { get; }
        public bool IsSponsored { get; }
        public int Priority { get; }
        public RestStream Stream { get; }
        public string Text { get; }
        public string Title { get; }
        
        public RestFeaturedStream(TwitchRestClient client, ulong id) : base(client, id) { }

        //IFeaturedStream
        IStream IFeaturedStream.Stream
            => Stream;
    }
}
