namespace NTwitch
{
    public interface IFeaturedStream : IEntity
    {
        string ImageUrl { get; }
        int Priority { get; }
        bool IsScheduled { get; }
        bool IsSponsored { get; }
        IStream Stream { get; }
        string Text { get; }
        string Title { get; }
    }
}
