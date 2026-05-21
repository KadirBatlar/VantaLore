using System.Net.Http.Json;
using VantaLore.Application.Interfaces;

namespace VantaLore.Infrastructure.AI;

public class OpenAIEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _http;

    public OpenAIEmbeddingService(HttpClient http)
    {
        _http = http;
    }

    public async Task<float[]> GetEmbeddingAsync(string text)
    {
        // API key eklenecek
        var response = await _http.PostAsJsonAsync(
            "https://api.openai.com/v1/embeddings",
            new
            {
                model = "text-embedding-3-small",
                input = text
            }
        );

        dynamic json = await response.Content.ReadFromJsonAsync<dynamic>();

        return json.data[0].embedding.ToObject<float[]>();
    }
}