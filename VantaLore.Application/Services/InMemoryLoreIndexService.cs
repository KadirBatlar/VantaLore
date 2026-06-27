using VantaLore.Application.Interfaces;
using VantaLore.Application.Models;
using VantaLore.Domain.Entities;

public class InMemoryLoreIndexService : ILoreIndexService
{
    private readonly List<IndexedLoreChunk> _index = [];

    public Task BuildIndexAsync(List<LoreChunk> chunks)
    {
        _index.AddRange(
            chunks.Select(x => new IndexedLoreChunk
            {
                Chunk = x,
                Embedding = x.Embedding
            }));

        return Task.CompletedTask;
    }

    public IReadOnlyList<IndexedLoreChunk> GetIndex()
        => _index;


}