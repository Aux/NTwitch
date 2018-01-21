using Model = NTwitch.Rest.API.Broadcast;

namespace NTwitch.Rest
{
    public class RestGameSummary
    {
        /// <summary> The total number of channels streaming this game </summary>
        public uint Channels { get; private set; }
        /// <summary> The total number of viewers watching channels streaming this game </summary>
        public uint Viewers { get; private set; }
        
        internal static RestGameSummary Create(Model model)
        {
            var entity = new RestGameSummary();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Channels = model.Channels;
            Viewers = model.Viewers;
        }
    }
}
