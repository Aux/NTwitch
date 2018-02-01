using System;
using System.Threading;
using System.Threading.Tasks;
using NTwitch.Rest;

namespace NTwitch.Helix.Webhook
{
    public interface IHttpListener
    {
        event Func<RestResponse, Task> RequestReceived;
        event Func<Exception, Task> Closed;

        void SetCancelToken(CancellationToken cancelToken);

        Task StartAsync(string callbackUrl);
        Task StopAsync(bool isDisposing = false);
    }
}
