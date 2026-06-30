using VantaLore.Application.Interfaces;
using VantaLore.Application.Models;

namespace VantaLore.Application.Services;

public sealed class EmbeddingRetrievalService : IRetrievalService
{
    private readonly IEmbeddingService _embedding;
    private readonly IVectorStore _vectorStore;

    public EmbeddingRetrievalService(IEmbeddingService embedding, IVectorStore vectorStore)
    {
        _embedding  = embedding;
        _vectorStore = vectorStore;
    }

    public async Task<IReadOnlyList<ScoredChunk>> RetrieveAsync(
        string query,
        int topK = 5,
        CancellationToken ct = default)
    {
        var queryVector = await _embedding.GetEmbeddingAsync(query, ct);
        return await _vectorStore.SearchAsync(queryVector, topK, ct);
    }
}