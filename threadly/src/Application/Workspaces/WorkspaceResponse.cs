namespace ApplyProjectStudio.Application.Workspaces;

public sealed record WorkspaceResponse(Guid Id, string Name, string? Description, DateTimeOffset CreatedAt);
