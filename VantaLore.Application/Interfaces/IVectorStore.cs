using VantaLore.Application.Models;

namespace VantaLore.Application.Interfaces;

public interface IVectorStore
{
    Task UpsertAsync(IEnumerable<EmbeddedChunk> chunks, CancellationToken ct = default);

    Task<IReadOnlyList<ScoredChunk>> SearchAsync(
        float[] queryVector,
        int topK = 5,
        CancellationToken ct = default);

    Task DeleteAsync(IEnumerable<int> ids, CancellationToken ct = default);

    Task<long> CountAsync(CancellationToken ct = default);
}
