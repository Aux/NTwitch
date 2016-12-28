namespace NTwitch
{
    public interface IBadge
    {
        string Name { get; }
        string AlphaUrl { get; }
        string ImageUrl { get; }
        string SvgUrl { get; }
    }
}
