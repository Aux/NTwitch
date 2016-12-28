namespace NTwitch
{
    public interface IIngest : IEntity
    {
        double Availability { get; }
        bool IsDefault { get; }
        string Name { get; }
        string UrlTemplate { get; }
    }
}
