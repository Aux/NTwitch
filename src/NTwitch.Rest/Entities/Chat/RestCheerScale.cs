using System.Collections.Generic;
using Model = NTwitch.Rest.API.CheerScale;

namespace NTwitch.Rest
{
    public class RestCheerScale : ICheerScale
    {
        /// <summary> The instance of the client that created this entity </summary>
        public BaseTwitchClient Client { get; }
        /// <summary> The urls for the animated versions of this cheer image </summary>
        public IReadOnlyDictionary<double, string> Animated { get; private set; }
        /// <summary> The urls for the static versions of this cheer image </summary>
        public IReadOnlyDictionary<double, string> Static { get; private set; }

        internal RestCheerScale(BaseTwitchClient client, Model model)
        {
            Client = client;
            Update(model);
        }
        
        internal virtual void Update(Model model)
        {
            Animated = model.Animated;
            Static = model.Static;
        }
    }
}
