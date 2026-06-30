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
        _baseUrl = configuration["Ollama:BaseUrl"]
            ?? throw new ArgumentNullException("Ollama:BaseUrl", "Ollama base URL is not configured.");
        _model   = configuration["Ollama:EmbeddingModel"]
            ?? throw new ArgumentNullException("Ollama:EmbeddingModel", "Ollama embedding model is not configured.");
    }

    public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken ct = default)
    {
        var response = await _http.PostAsJsonAsync(
            $"{_baseUrl}/api/embeddings",
            new
            {
                model  = _model,
                prompt = text
            },
            ct);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<OllamaEmbeddingsResponse>(cancellationToken: ct)
            ?? throw new InvalidOperationException(
                "Ollama returned a null response from /api/embeddings.");

        if (result.Embedding is null || result.Embedding.Length == 0)
            throw new InvalidOperationException(
                "Ollama returned an empty embedding from /api/embeddings.");

        return result.Embedding;
    }

    private sealed class OllamaEmbeddingsResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("embedding")]
        public float[]? Embedding { get; set; }
    }
}