using ApplyProjectStudio.Domain.Workspaces;

namespace ApplyProjectStudio.Application.Workspaces;

public interface IWorkspaceRepository
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task AddAsync(Workspace workspace, CancellationToken cancellationToken);
}
