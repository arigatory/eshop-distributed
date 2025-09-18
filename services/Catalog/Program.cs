using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductAIService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());
// builder.AddOllamaSharpChatClient("ollama-llama3-2");
builder.AddOllamaApiClient("ollama-llama3-2").AddChatClient();
// builder.AddOllamaSharpEmbeddingGenerator("ollama-all-mimilm");
builder.AddOllamaApiClient("ollama-all-minilm").AddEmbeddingGenerator();

builder.Services.AddInMemoryVectorStore();
builder.Services.AddInMemoryVectorStoreRecordCollection<int, ProductVector>("products");


builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseMigration();
app.MapProductEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
