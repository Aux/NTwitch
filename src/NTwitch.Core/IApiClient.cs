using System.Threading.Tasks;

namespace NTwitch
{
    public interface IApiClient
    {
        Task SendAsync(RestRequest request);
        Task<T> SendAsync<T>(RestRequest request);
    }
}
