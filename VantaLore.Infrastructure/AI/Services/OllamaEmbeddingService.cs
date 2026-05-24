using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using VantaLore.Application.Interfaces;

namespace VantaLore.Infrastructure.AI.Services;

public class OllamaEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _http;
    private readonly string _model;
    private readonly string _baseUrl;

    public OllamaEmbeddingService(HttpClient http, IConfiguration configuration)
    {
        _http    = http;
        _baseUrl = configuration["Ollama:BaseUrl"];
        _model   = configuration["Ollama:EmbeddingModel"];
    }

    public async Task<float[]> GetEmbeddingAsync(string text)
    {
        var response = await _http.PostAsJsonAsync(
            $"{_baseUrl}/api/embeddings",
            new
            {
                model  = _model,
                prompt = text
            });

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<OllamaEmbeddingsResponse>()
            ?? throw new InvalidOperationException(
                "Ollama /api/embeddings null response döndürdü.");

        if (result.embedding is null || result.embedding.Length == 0)
            throw new InvalidOperationException(
                "Ollama /api/embeddings boş embedding döndürdü.");

        return result.embedding;
    }

    private sealed class OllamaEmbeddingsResponse
    {
        public float[]? embedding { get; set; }
    }
}