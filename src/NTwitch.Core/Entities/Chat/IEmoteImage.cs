namespace NTwitch
{
    public interface IEmoteImage
    {
        int Width { get; set; }
        int Height { get; set; }
        string ImageUrl { get; set; }
        ulong SetId { get; set; }
    }
}
