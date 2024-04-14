using System.Net.Http;
using BookShelf.Domain.Rest;
using Newtonsoft.Json;

namespace BookShelf.Infrastructure.Rest
{
    internal class ApiRequestExecutor : IApiRequestExecutor
    {
        private readonly Uri _baseAddres = new("http://localhost:50000");
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiRequestExecutor(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResponse> GetAsync<TResponse>(string reguest)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = _baseAddres;

            var httpResponseMessage = await httpClient.GetAsync(reguest);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(content);

            if (response != null)
                return response;

            throw new InvalidOperationException("Response can't be null");

        }
    }
}
