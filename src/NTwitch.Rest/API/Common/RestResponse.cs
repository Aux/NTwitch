using Newtonsoft.Json;
using System.Net;

namespace NTwitch.Rest
{
    public class RestResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Body { get; }
        public double ExecuteTime { get; }

        public RestResponse(HttpStatusCode code, string body, double time)
        {
            StatusCode = code;
            Body = body;
            ExecuteTime = time;
        }

        public T GetBodyAsType<T>()
            => JsonConvert.DeserializeObject<T>(Body);
    }
}
