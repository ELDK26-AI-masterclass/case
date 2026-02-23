using ApplyProjectStudio.Application.Workspaces;
using ApplyProjectStudio.Domain.Workspaces;
using ApplyProjectStudio.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApplyProjectStudio.Infrastructure.Workspaces;

public sealed class WorkspaceRepository : IWorkspaceRepository
{
    private readonly AppDbContext _dbContext;

    public WorkspaceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var normalized = name.Trim();

        return _dbContext.Workspaces.AnyAsync(workspace => workspace.Name == normalized, cancellationToken);
    }

    public async Task AddAsync(Workspace workspace, CancellationToken cancellationToken)
    {
        _dbContext.Workspaces.Add(workspace);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
