using VantaLore.Domain.Entities;

namespace VantaLore.Application.Models;

public sealed class EmbeddedChunk
{
    public required int Id { get; init; }
    public required float[] Embedding { get; init; }
    public required LoreChunk Chunk { get; init; }
}
