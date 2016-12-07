using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    internal class RestApiClient
    {
        private string _clientid;
        private string _baseurl;

        internal RestApiClient(string clientid, string baseurl)
        {
            _clientid = clientid;
            _baseurl = baseurl;
        }

        internal async Task SendAsync(string method, string endpoint, object payload)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_baseurl);
                http.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v2+json");
                http.DefaultRequestHeaders.Add("Client-ID", _clientid);

                var request = new HttpRequestMessage(new HttpMethod(method), endpoint);

                var response = await http.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"{(int)response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        internal async Task<T> SendAsync<T>(string method, string endpoint, object payload)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_baseurl);
                http.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v2+json");
                http.DefaultRequestHeaders.Add("Client-ID", _clientid);

                var request = new HttpRequestMessage(new HttpMethod(method), endpoint);

                var response = await http.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"{(int)response.StatusCode}: {response.ReasonPhrase}");
                } else
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
        }

        internal async Task LoginAsync()
        {
            await Task.Delay(1);
        }
    }
}
