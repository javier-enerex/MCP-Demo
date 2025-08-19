using PPMcpDemo.OpenApi;
using PPMcpDemo.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMcpServer()
                .WithHttpTransport(o => o.Stateless = true)
                .WithToolsFromAssembly();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IOpportunityService, OpportunityService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddHttpClient<OpportunityService>();

builder.Services.AddOpenApi("swagger", o =>
{
    o.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    o.AddDocumentTransformer<McpDocumentTransformer>();
});

builder.Services.AddOpenApi("openapi", o =>
{
    o.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
    o.AddDocumentTransformer<McpDocumentTransformer>();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapOpenApi("/{documentName}.json");

app.MapMcp("/mcp");

app.Run();
