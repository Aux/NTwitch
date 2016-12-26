namespace NTwitch
{
    public interface IBadge
    {
        string Name { get; set; }
        string AlphaUrl { get; set; }
        string ImageUrl { get; set; }
        string SvgUrl { get; set; }
    }
}
