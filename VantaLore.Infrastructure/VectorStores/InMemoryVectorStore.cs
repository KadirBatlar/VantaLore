using VantaLore.Application.Interfaces;
using VantaLore.Application.Models;

namespace VantaLore.Infrastructure.VectorStores;

/// <summary>
/// An in-memory vector store backed by a list with linear cosine similarity search.
/// Suitable for development, testing, and small datasets.
///
/// Production deployments should replace this with a provider-specific implementation
/// (e.g. Qdrant, PostgreSQL + pgvector) without changing any Application code.
/// </summary>
public sealed class InMemoryVectorStore : IVectorStore
{
    // Not thread-safe for concurrent writes, but safe for concurrent reads after warm-up.
    // Replace with ConcurrentDictionary if you add live indexing endpoints.
    private readonly List<EmbeddedChunk> _store = [];

    public Task UpsertAsync(IEnumerable<EmbeddedChunk> chunks, CancellationToken ct = default)
    {
        foreach (var chunk in chunks)
        {
            // Remove any existing entry with the same ID so upsert is idempotent.
            _store.RemoveAll(x => x.Id == chunk.Id);
            _store.Add(chunk);
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<ScoredChunk>> SearchAsync(
        float[] queryVector,
        int topK = 5,
        CancellationToken ct = default)
    {
        IReadOnlyList<ScoredChunk> results = _store
            .Select(x => new ScoredChunk
            {
                Chunk = x.Chunk,
                Score = CosineSimilarity(x.Embedding, queryVector)
            })
            .OrderByDescending(x => x.Score)
            .Take(topK)
            .ToList();

        return Task.FromResult(results);
    }

    public Task DeleteAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        var idSet = ids.ToHashSet();
        _store.RemoveAll(x => idSet.Contains(x.Id));
        return Task.CompletedTask;
    }

    public Task<long> CountAsync(CancellationToken ct = default)
        => Task.FromResult((long)_store.Count);

    private static double CosineSimilarity(float[] a, float[] b)
    {
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
