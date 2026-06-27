using VantaLore.Application.Interfaces;
using VantaLore.Domain.Entities;

public class EmbeddingRetrievalService : IRetrievalService
{
    private readonly IEmbeddingService _embedding;
    private readonly ILoreIndexService _indexService;

    public EmbeddingRetrievalService(
        IEmbeddingService embedding,
        ILoreIndexService indexService)
    {
        _embedding = embedding;
        _indexService = indexService;
    }

    public async Task<List<LoreChunk>> Retrieve(string query)
    {

        var queryVector = await _embedding.GetEmbeddingAsync(query);        

        var chunks = _indexService.GetIndex();

        return chunks
            .OrderByDescending(x => CosineSimilarity(x.Embedding, queryVector))
            .Take(3)
            .Select(x => x.Chunk)
            .ToList();
    }

    private static double CosineSimilarity(float[]? a, float[]? b)
    {
        if (a is null || b is null) return 0;
        if (a.Length != b.Length) return 0;

        double dot = 0, magA = 0, magB = 0;

        for (int i = 0; i < a.Length; i++)
        {
            dot  += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }

        var denom = Math.Sqrt(magA) * Math.Sqrt(magB);
        return denom == 0 ? 0 : dot / denom;
    }
}