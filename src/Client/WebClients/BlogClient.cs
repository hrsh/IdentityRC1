using Shared;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Client.WebClients
{
    public class BlogClient
    {
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _client;

        public BlogClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Blog[]> GetBlogsAsync(string token, CancellationToken ct)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _client.GetAsync("api/v2/blog/list", ct);
            responseMessage.EnsureSuccessStatusCode();
            var stream = await responseMessage.Content.ReadAsStreamAsync(ct);
            return await JsonSerializer.DeserializeAsync<Blog[]>(stream, _options, ct);
        }
    }
}
