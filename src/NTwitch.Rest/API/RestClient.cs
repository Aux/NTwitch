using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestClient : IDisposable
    {
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }

        internal Task LoginAsync(string clientid)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
