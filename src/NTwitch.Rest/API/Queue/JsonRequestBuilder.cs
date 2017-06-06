namespace NTwitch.Rest.Queue
{
    public class JsonRequestBuilder : RequestBuilder
    {
        public object Payload => _payload;

        private object _payload;

        public JsonRequestBuilder(string method, string endpoint, object payload) 
            : base(method, endpoint)
        {
            _payload = payload;
        }

        public void SetPayload(object payload)
        {
            _payload = payload;
        }
    }
}
