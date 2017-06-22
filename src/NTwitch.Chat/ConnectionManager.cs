using System;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal class ConnectionManager
    {
        public event Func<Task> Connected { add { _connectedEvent.Add(value); } remove { _connectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _connectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Exception, bool, Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Exception, bool, Task>> _disconnectedEvent = new AsyncEvent<Func<Exception, bool, Task>>();

        private readonly SemaphoreSlim _stateLock;
        private readonly Logger _logger;
        private readonly int _connectionTimeout;
        private readonly Func<Task> _connecting;
        private readonly Func<Exception, Task> _disconnecting;

        private TaskCompletionSource<bool> _connectionPromise;
        private CancellationTokenSource _combinedCancelToken, _reconnectCancelToken, _connectionCancelToken;
        private Task _task;

        public ConnectionState State { get; private set; }
        public CancellationToken CancelToken { get; private set; }

        internal ConnectionManager(SemaphoreSlim stateLock, Logger logger, int connectionTimeout,
            Func<Task> onConnecting, Func<Exception, Task> onDisconnecting, Action<Func<Exception, Task>> clientDisconnectHandler)
        {
            _stateLock = stateLock;
            _logger = logger;
            _connectionTimeout = connectionTimeout;
            _connecting = onConnecting;
            _disconnecting = onDisconnecting;

            clientDisconnectHandler(ex =>
            {
                if (ex != null)
                    Error(new Exception("Socket connection was closed", ex));
                else
                    Error(new Exception("Socket connection was closed"));
                return Task.Delay(0);
            });
        }

        public virtual async Task StartAsync()
        {
            await AcquireConnectionLock().ConfigureAwait(false);
            var reconnectCancelToken = new CancellationTokenSource();
            _reconnectCancelToken = reconnectCancelToken;
            _task = Task.Run(async () =>
            {
                try
                {
                    Random jitter = new Random();
                    int nextReconnectDelay = 1000;
                    while (!reconnectCancelToken.IsCancellationRequested)
                    {
                        try
                        {
                            await ConnectAsync(reconnectCancelToken).ConfigureAwait(false);
                            nextReconnectDelay = 1000;                         
                            await _connectionPromise.Task.ConfigureAwait(false);
                        }
                        catch (OperationCanceledException ex)
                        {
                            Cancel();
                            await DisconnectAsync(ex, !reconnectCancelToken.IsCancellationRequested).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Error(ex);
                            if (!reconnectCancelToken.IsCancellationRequested)
                            {
                                await _logger.WarningAsync(ex).ConfigureAwait(false);
                                await DisconnectAsync(ex, true).ConfigureAwait(false);
                            }
                            else
                            {
                                await _logger.ErrorAsync(ex).ConfigureAwait(false);
                                await DisconnectAsync(ex, false).ConfigureAwait(false);
                            }
                        }

                        if (!reconnectCancelToken.IsCancellationRequested)
                        {
                            await Task.Delay(nextReconnectDelay, reconnectCancelToken.Token).ConfigureAwait(false);
                            nextReconnectDelay = (nextReconnectDelay * 2) + jitter.Next(-250, 250);
                            if (nextReconnectDelay > 60000)
                                nextReconnectDelay = 60000;
                        }
                    }
                }
                finally { _stateLock.Release(); }
            });
        }

        public virtual async Task StopAsync()
        {
            Cancel();
            var task = _task;
            if (task != null)
                await task.ConfigureAwait(false);
        }

        private async Task ConnectAsync(CancellationTokenSource reconnectCancelToken)
        {
            _connectionCancelToken = new CancellationTokenSource();
            _combinedCancelToken = CancellationTokenSource.CreateLinkedTokenSource(_connectionCancelToken.Token, reconnectCancelToken.Token);
            CancelToken = _combinedCancelToken.Token;

            _connectionPromise = new TaskCompletionSource<bool>();
            State = ConnectionState.Connecting;
            await _logger.InfoAsync("Connecting").ConfigureAwait(false);

            try
            {
                await _connecting().ConfigureAwait(false);

                await _logger.InfoAsync("Connected").ConfigureAwait(false);
                State = ConnectionState.Connected;
                await _logger.DebugAsync("Raising Event").ConfigureAwait(false);
                await _connectedEvent.InvokeAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }
        }

        private async Task DisconnectAsync(Exception ex, bool isReconnecting)
        {
            if (State == ConnectionState.Disconnected) return;
            State = ConnectionState.Disconnecting;
            await _logger.InfoAsync("Disconnecting").ConfigureAwait(false);

            await _disconnecting(ex).ConfigureAwait(false);

            await _logger.InfoAsync("Disconnected").ConfigureAwait(false);
            State = ConnectionState.Disconnected;
            await _disconnectedEvent.InvokeAsync(ex, isReconnecting).ConfigureAwait(false);
        }
        
        public void Cancel()
        {
            _connectionPromise?.TrySetCanceled();
            _reconnectCancelToken?.Cancel();
            _connectionCancelToken?.Cancel();
        }

        public void Error(Exception ex)
        {
            _connectionPromise.TrySetException(ex);
            _connectionCancelToken?.Cancel();
        }

        public void CriticalError(Exception ex)
        {
            _reconnectCancelToken?.Cancel();
            Error(ex);
        }

        public void Reconnect()
        {
            _connectionPromise.TrySetCanceled();
            _connectionCancelToken?.Cancel();
        }

        private async Task AcquireConnectionLock()
        {
            while (true)
            {
                await StopAsync().ConfigureAwait(false);
                if (await _stateLock.WaitAsync(0).ConfigureAwait(false))
                    break;
            }
        }
    }
}
