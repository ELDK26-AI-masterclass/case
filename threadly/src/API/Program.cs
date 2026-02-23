using ApplyProjectStudio.API;
using ApplyProjectStudio.Application.Workspaces;
using ApplyProjectStudio.Infrastructure.Persistence;
using ApplyProjectStudio.Infrastructure.Workspaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("defaultconnection")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("A PostgreSQL connection string was not configured. Set ConnectionStrings:defaultconnection or ConnectionStrings:DefaultConnection.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiExceptionHandlingMiddleware>();

app.MapPost("/api/v1/workspaces", async (
    CreateWorkspaceRequest request,
    IWorkspaceService workspaceService,
    CancellationToken cancellationToken) =>
{
    var response = await workspaceService.CreateWorkspaceAsync(request, cancellationToken);

    return Results.Created($"/api/v1/workspaces/{response.Id}", response);
});

app.Run();
