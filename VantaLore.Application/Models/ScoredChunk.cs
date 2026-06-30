using VantaLore.Domain.Entities;

namespace VantaLore.Application.Models;

public sealed class ScoredChunk
{
    public required LoreChunk Chunk { get; init; }

    /// Similarity score in [0, 1] for cosine
    public double Score { get; init; }
}
