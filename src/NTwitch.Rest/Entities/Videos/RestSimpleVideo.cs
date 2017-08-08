using System;
using Model = NTwitch.Rest.API.Video;

namespace NTwitch.Rest
{
    public class RestSimpleVideo : RestEntity<string>, IEquatable<RestSimpleVideo>
    {
        /// <summary> The url to this video's page </summary>
        public string Url { get; private set; }

        internal RestSimpleVideo(BaseTwitchClient client, string id) 
            : base(client, id) { }

        public bool Equals(RestSimpleVideo other)
            => Id == other.Id;

        internal static RestVideo Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestVideo(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Url = model.Url;
        }
    }
}
