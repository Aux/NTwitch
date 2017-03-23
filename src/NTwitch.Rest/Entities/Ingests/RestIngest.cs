using Model = NTwitch.Rest.API.Ingest;

namespace NTwitch.Rest
{
    public class RestIngest : RestEntity<ulong>
    {
        public double Availability { get; private set; }
        public bool IsDefault { get; private set; }
        public string Name { get; private set; }
        public string UrlTemplate { get; private set; }

        public RestIngest(BaseRestClient client, ulong id)
            : base(client, id) { }
        
        internal static RestIngest Create(BaseRestClient client, Model model)
        {
            var entity = new RestIngest(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Availability = model.Availability;
            IsDefault = model.IsDefault;
            Name = model.Name;
            UrlTemplate = model.UrlTemplate;
        }
    }
}
