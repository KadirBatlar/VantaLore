using VantaLore.Application.Interfaces;
using VantaLore.Domain.Entities;

public class EmbeddingRetrievalService : IRetrievalService
{
    private readonly ILoreRepository _repo;
    private readonly IEmbeddingService _embedding;

    public EmbeddingRetrievalService(
        ILoreRepository repo,
        IEmbeddingService embedding)
    {
        _repo = repo;
        _embedding = embedding;
    }

    public async Task<List<LoreChunk>> Retrieve(string query)
    {
        var data = _repo.GetAll();

        var queryVector = await _embedding.GetEmbeddingAsync(query);

        foreach (var chunk in data)
        {
            if (chunk.Embedding == null)
                chunk.Embedding = await _embedding.GetEmbeddingAsync(chunk.Content);
        }

        return data
            .OrderByDescending(x => CosineSimilarity(x.Embedding, queryVector))
            .Take(3)
            .ToList();
    }

    private double CosineSimilarity(float[] a, float[] b)
    {
        double dot = 0, magA = 0, magB = 0;

        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }

        return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
    }
}