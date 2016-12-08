namespace Twitch
{
    public interface IBadge
    {
        string Name { get; }
        string AlphaUrl { get; }
        string ImageUrl { get; }
        string SvgUrl { get; }
    }
}
