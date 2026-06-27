using VantaLore.Domain.Entities;

namespace VantaLore.Application.Models
{
    public class IndexedLoreChunk
    {
        public LoreChunk Chunk { get; init; }

        public float[] Embedding { get; init; } = [];
    }
}