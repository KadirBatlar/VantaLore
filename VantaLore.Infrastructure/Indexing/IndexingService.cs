using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VantaLore.Application.Interfaces;
using VantaLore.Application.Models;

namespace VantaLore.Infrastructure.Indexing;

public sealed class IndexingService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IVectorStore _vectorStore;
    private readonly ILogger<IndexingService> _logger;


    public IndexingService(
        IServiceScopeFactory scopeFactory,
        IVectorStore vectorStore,
        ILogger<IndexingService> logger)
    {
        _scopeFactory = scopeFactory;
        _vectorStore  = vectorStore;
        _logger       = logger;
    }

    public async Task StartAsync(CancellationToken ct)
    {
        _logger.LogInformation("Building lore index...");

        using var scope = _scopeFactory.CreateScope();

        var repo      = scope.ServiceProvider.GetRequiredService<ILoreRepository>();
        var embedding = scope.ServiceProvider.GetRequiredService<IEmbeddingService>();

        var chunks = repo.GetAll();

        var embeddedChunks = new List<EmbeddedChunk>(chunks.Count);

        foreach (var chunk in chunks)
        {
            var vector = await embedding.GetEmbeddingAsync(chunk.Content ?? string.Empty, ct);

            embeddedChunks.Add(new EmbeddedChunk
            {
                Id        = chunk.Id,
                Embedding = vector,
                Chunk     = chunk
            });
        }

        await _vectorStore.UpsertAsync(embeddedChunks, ct);

        _logger.LogInformation(
            "Lore index ready — {Count} chunk(s) indexed.",
            embeddedChunks.Count);
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
