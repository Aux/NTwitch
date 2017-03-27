using Model = NTwitch.Rest.API.Stream;

namespace NTwitch.Rest
{
    public class RestStreamSummary
    {
        public uint Channels { get; private set; }
        public uint Viewers { get; private set; }
        
        internal static RestStreamSummary Create(Model model)
        {
            var entity = new RestStreamSummary();
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
