namespace Twitch
{
    public interface ITopGame : IGame
    {
        int Viewers { get; }
        int Channels { get; }
    }
}
