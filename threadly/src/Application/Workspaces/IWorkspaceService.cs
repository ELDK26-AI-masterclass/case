namespace ApplyProjectStudio.Application.Workspaces;

public interface IWorkspaceService
{
    Task<WorkspaceResponse> CreateWorkspaceAsync(CreateWorkspaceRequest request, CancellationToken cancellationToken);
}
