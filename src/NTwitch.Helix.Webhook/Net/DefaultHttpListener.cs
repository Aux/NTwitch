using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NTwitch.Rest;

namespace NTwitch.Helix.Webhook
{
    internal sealed class DefaultHttpListener : IHttpListener, IDisposable
    {
        public event Func<RestResponse, Task> RequestReceived;
        public event Func<Exception, Task> Closed;

        private readonly SemaphoreSlim _lock;
        private HttpListener _client;
        private Task _task;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _cancelToken, _parentToken;
        private bool _isDisposed, _isDisconnecting;

        public DefaultHttpListener()
        {
            _lock = new SemaphoreSlim(1, 1);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelToken = CancellationToken.None;
            _parentToken = CancellationToken.None;
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                    _client.Close();
                _isDisposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }

        public async Task StartAsync(string callbackUrl)
        {
            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StartInternalAsync(callbackUrl).ConfigureAwait(false);
            }
            finally
            {
                _lock.Release();
            }
        }
        private async Task StartInternalAsync(string callbackUrl)
        {
            await StopInternalAsync().ConfigureAwait(false);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelToken = CancellationTokenSource.CreateLinkedTokenSource(_parentToken, _cancelTokenSource.Token).Token;

            _client = new HttpListener();
            _client.Prefixes.Add(callbackUrl);
            _client.Start();
            _task = ListenAsync(_cancelToken);
        }
        public async Task StopAsync(bool isDisposing = false)
        {
            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopInternalAsync().ConfigureAwait(false);
            }
            finally
            {
                _lock.Release();
            }
        }
        private async Task StopInternalAsync(bool isDisposing = false)
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            _isDisconnecting = true;
            try
            {
                await (_task ?? Task.Delay(0)).ConfigureAwait(false);
                _task = null;
            }
            finally { _isDisconnecting = false; }

            if (_client != null)
            {
                if (!isDisposing)
                {
                    try { _client.Abort(); }
                    catch { }
                }

                _client = null;
            }
        }
        private async Task OnClosed(Exception ex)
        {
            if (_isDisconnecting)
                return; //Ignore, this disconnect was requested.

            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopInternalAsync(false).ConfigureAwait(false);
            }
            finally
            {
                _lock.Release();
            }
            await Closed(ex);
        }

        public void SetCancelToken(CancellationToken cancelToken)
        {
            _parentToken = cancelToken;
            _cancelToken = CancellationTokenSource.CreateLinkedTokenSource(_parentToken, _cancelTokenSource.Token).Token;
        }

        private async Task ListenAsync(CancellationToken cancelToken)
        {
            try
            {
                while(!cancelToken.IsCancellationRequested)
                {
                    var context = await _client.GetContextAsync();
                    
                    context.Response.AddHeader("Date", DateTime.UtcNow.ToString());
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentLength64 = 0;

                    await context.Response.OutputStream.FlushAsync();
                    context.Response.OutputStream.Close();

                    var request = context.Request;
                    _ = RequestReceived(new RestResponse(HttpStatusCode.OK, request.Headers.ToDictionary(), request.InputStream));
                }
            }
            catch (Exception ex)
            {
                _ = OnClosed(ex);
            }
        }
    }
}
