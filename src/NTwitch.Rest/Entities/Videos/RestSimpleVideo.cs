using Model = NTwitch.Rest.API.Video;

namespace NTwitch.Rest
{
    public class RestSimpleVideo : RestEntity<string>, ISimpleVideo
    {
        /// <summary> The url to this video's page </summary>
        public string Url { get; private set; }

        internal RestSimpleVideo(BaseTwitchClient client, string id) 
            : base(client, id) { }

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

        // IEqualityComparer
        public bool Equals(ISimpleVideo x, ISimpleVideo y) => x.Id == y.Id;
        public int GetHashCode(ISimpleVideo obj) => obj.GetHashCode();
    }
}
