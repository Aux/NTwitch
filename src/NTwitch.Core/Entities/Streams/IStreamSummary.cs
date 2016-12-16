namespace NTwitch
{
    public interface IStreamSummary
    {
        int Channels { get; }
        int Viewers { get; }
        string[] Links { get; }
    }
}
