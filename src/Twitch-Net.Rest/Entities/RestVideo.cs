using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class RestVideo : IVideo
    {
        public uint BroadcastId { get; }
        public IChannel Channel { get; }
        public string Description { get; }
        public string Game { get; }
        public string Id { get; }
        public int Length { get; }
        public string[] Links { get; }
        public string PreviewUrl { get; }
        public DateTime RecordedAt { get; }
        public string Status { get; }
        public string[] Tags { get; }
        public string Title { get; }
        public BroadcastType Type { get; }
        public string Url { get; }
        public int Views { get; }
    }
}
