using System.IO;
using System.Net;

namespace NTwitch
{
    public class RestResponse
    {
        public HttpStatusCode StatusCode { get; }
        public Stream Body { get; }

        public RestResponse(HttpStatusCode code, Stream body)
        {
            StatusCode = code;
            Body = body;
        }
    }
}
