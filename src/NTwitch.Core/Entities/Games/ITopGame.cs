namespace NTwitch
{
    public interface ITopGame : IGame
    {
        int Viewers { get; }
        int Channels { get; }
    }
}
