using VantaLore.Application.Interfaces;

namespace VantaLore.Infrastructure.AI;

public class FakeEmbeddingService : IEmbeddingService
{
    public Task<float[]> GetEmbeddingAsync(string text, CancellationToken ct = default)
    {
        var hash = text.GetHashCode();

        var vector = new float[10];

        var rand = new Random(hash);

        for (int i = 0; i < vector.Length; i++)
            vector[i] = (float)rand.NextDouble();

        return Task.FromResult(vector);
    }
}