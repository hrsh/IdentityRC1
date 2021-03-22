using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.WebClients
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

        public async Task<Shared.Blog[]> GetBlogsAsync(CancellationToken ct)
        {
            var responseMessage = await _client.GetAsync("/list", ct);
            var stream = await responseMessage.Content.ReadAsStreamAsync(ct);
            return await JsonSerializer.DeserializeAsync<Shared.Blog[]>(stream, _options, cancellationToken: ct);
        }
    }
}
