using VantaLore.Application.Interfaces;
using VantaLore.Application.Services;
using VantaLore.Infrastructure.AI.Services;
using VantaLore.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "VantaLore API",
        Version = "v1",
        Description = "RAG-powered lore assistant backend."
    });
});

builder.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(60);
});

// Application
builder.Services.AddScoped<LoreQueryService>();
builder.Services.AddScoped<IRetrievalService, EmbeddingRetrievalService>();
builder.Services.AddSingleton<ILoreIndexService, InMemoryLoreIndexService>();

// Infrastructure
builder.Services.AddScoped<IEmbeddingService, OllamaEmbeddingService>();
builder.Services.AddScoped<ILoreRepository, LoreRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<ILoreRepository>();
    var embedding = scope.ServiceProvider.GetRequiredService<IEmbeddingService>();
    var index = scope.ServiceProvider.GetRequiredService<ILoreIndexService>();

    var chunks = repo.GetAll();

    foreach (var chunk in chunks)
    {
        chunk.Embedding = await embedding.GetEmbeddingAsync(chunk.Content);
    }

    await index.BuildIndexAsync(chunks);
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "VantaLore API v1");
    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
