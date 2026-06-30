using VantaLore.Application.Interfaces;
using VantaLore.Application.Services;
using VantaLore.Infrastructure.AI.Services;
using VantaLore.Infrastructure.Indexing;
using VantaLore.Infrastructure.Repositories;
using VantaLore.Infrastructure.VectorStores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title       = "VantaLore API",
        Version     = "v1",
        Description = "RAG-powered lore assistant backend."
    });
});

// ── Embedding
builder.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(60);
});

// ── Vector Store
builder.Services.AddSingleton<IVectorStore, InMemoryVectorStore>();

// ── Application Services
builder.Services.AddScoped<IRetrievalService, EmbeddingRetrievalService>();
builder.Services.AddScoped<LoreQueryService>();

// ── Infrastructure
builder.Services.AddScoped<ILoreRepository, LoreRepository>();

// ── Startup Indexing
builder.Services.AddHostedService<IndexingService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "VantaLore API v1");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();