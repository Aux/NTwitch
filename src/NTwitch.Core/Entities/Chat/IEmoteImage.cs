namespace NTwitch
{
    public interface IEmoteImage
    {
        int Width { get; }
        int Height { get; }
        string ImageUrl { get; }
        ulong SetId { get; }
    }
}
