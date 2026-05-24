using Microsoft.Extensions.Configuration;
using OpenAI.Embeddings;
using VantaLore.Application.Interfaces;

namespace VantaLore.Infrastructure.AI;

public class OpenAIEmbeddingService : IEmbeddingService
{
    private readonly EmbeddingClient _client;

    public OpenAIEmbeddingService(IConfiguration configuration)
    {
        var apiKey = configuration["OpenAI:ApiKey"];

        _client = new EmbeddingClient(
            "text-embedding-3-small",
            apiKey);
    }

    public async Task<float[]> GetEmbeddingAsync(string text)
    {
        var response =
            await _client.GenerateEmbeddingAsync(text);

        return response.Value
            .ToFloats()
            .ToArray();
    }
}