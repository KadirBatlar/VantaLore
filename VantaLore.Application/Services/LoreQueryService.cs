using VantaLore.Application.Interfaces;
using VantaLore.Domain.Entities;

namespace VantaLore.Application.Services;

//sealed = cannot be inherited
public sealed class LoreQueryService
{
    private readonly IRetrievalService _retrievalService;

    public LoreQueryService(IRetrievalService retrievalService)
    {
        _retrievalService = retrievalService;
    }

    ///Returns the top 5 matching lore chunks 
    public async Task<IReadOnlyList<LoreChunk>> Search(string query, CancellationToken ct = default)
    {
        var results = await _retrievalService.RetrieveAsync(query, topK: 5, ct);
        return results.Select(x => x.Chunk).ToList();
    }

    ///Returns the single best matching chunk
    public async Task<LoreChunk?> Ask(string question, CancellationToken ct = default)
    {
        var results = await _retrievalService.RetrieveAsync(question, topK: 1, ct);
        return results.FirstOrDefault()?.Chunk;
    }
}