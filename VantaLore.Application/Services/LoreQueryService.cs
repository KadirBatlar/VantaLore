using VantaLore.Application.Interfaces;
using VantaLore.Domain.Entities;

namespace VantaLore.Application.Services;

public class LoreQueryService
{
    private readonly IRetrievalService _retrievalService;

    public LoreQueryService(IRetrievalService retrievalService)
    {
        _retrievalService = retrievalService;
    }

    public async Task<List<LoreChunk>> Search(string query)
    {
        return await _retrievalService.Retrieve(query);
    }

    public async Task<LoreChunk?> Ask(string question)
    {
        return (await _retrievalService.Retrieve(question))
            .FirstOrDefault();
    }
}