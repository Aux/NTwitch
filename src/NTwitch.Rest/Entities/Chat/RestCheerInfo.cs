using System;
using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Rest.API.CheerInfo;

namespace NTwitch.Rest
{
    public class RestCheerInfo
    {
        /// <summary> The instance of the client that created this entity </summary>
        public BaseTwitchClient Client { get; }
        /// <summary>  </summary>
        public string Prefix { get; private set; }
        /// <summary> Alternate backgrounds for this cheer's image </summary>
        public IReadOnlyCollection<string> Backgrounds { get; private set; }
        /// <summary> Alternate sizes for this cheer's image </summary>
        public IReadOnlyCollection<double> Scales { get; private set; }
        /// <summary>  </summary>
        public IReadOnlyCollection<string> States { get; private set; }
        /// <summary>  </summary>
        public IReadOnlyCollection<RestCheer> Tiers { get; private set; }
        /// <summary> Date and time when this cheer was last updated </summary>
        public DateTime UpdatedAt { get; private set; }
        /// <summary>  </summary>
        public string Type { get; private set; }

        internal RestCheerInfo(BaseTwitchClient client, Model model)
        {
            Client = client;
        }
        
        internal virtual void Update(Model model)
        {
            Prefix = model.Prefix;
            Backgrounds = model.Backgrounds.ToArray();
            Scales = model.Scales.ToArray();
            States = model.States.ToArray();
            UpdatedAt = model.UpdatedAt;
            Type = model.Type;
            Tiers = model.Tiers.Select(x =>
            {
                var cheer = new RestCheer(Client, x.Id);
                cheer.Update(x);
                return cheer;
            }).ToArray();
        }
    }
}
