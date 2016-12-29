using System;

namespace NTwitch
{
    public interface IVideo : IEntity
    {
        ulong BroadcastId { get; }
        BroadcastType Type { get; }
        IChannelSummary Channel { get; }
        DateTime CreatedAt { get; }
        string Description { get; }
        string DescriptionRaw { get; }
        string Game { get; }
        string Language { get; }
        int Length { get; }
        TwitchImage Preview { get; }
        DateTime PublishedAt { get; }
        string Status { get; }
        string[] Tags { get; }
        string Title { get; }
        string Url { get; }
        string Viewable { get; }
        int Views { get; }
    }
}
