using VantaLore.Application.Models;

namespace VantaLore.Application.Interfaces;

public interface IRetrievalService
{
    Task<IReadOnlyList<ScoredChunk>> RetrieveAsync(
        string query,
        int topK = 5,
        CancellationToken ct = default);
}