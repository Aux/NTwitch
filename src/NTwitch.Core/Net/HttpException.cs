using System;
using System.Net;

namespace NTwitch
{
    public class HttpException : Exception
    {
        public HttpStatusCode HttpCode { get; }
        public string Reason { get; }

        public HttpException(HttpStatusCode httpCode, string reason = null)
            : base(CreateMessage(httpCode, reason))
        {
            HttpCode = httpCode;
            Reason = reason;
        }

        private static string CreateMessage(HttpStatusCode httpCode, string reason = null)
        {
            if (reason != null)
                return $"The server responded with error {(int)httpCode}: {reason}";
            else
                return $"The server responded with error {(int)httpCode}: {httpCode}";
        }
    }
}
