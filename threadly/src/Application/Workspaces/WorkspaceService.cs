using ApplyProjectStudio.Domain.Workspaces;

namespace ApplyProjectStudio.Application.Workspaces;

public sealed class WorkspaceService : IWorkspaceService
{
    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkspaceService(IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public async Task<WorkspaceResponse> CreateWorkspaceAsync(CreateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        if (await _workspaceRepository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            throw new WorkspaceAlreadyExistsException(request.Name);
        }

        var workspace = Workspace.Create(request.Name, request.Description, DateTimeOffset.UtcNow);

        await _workspaceRepository.AddAsync(workspace, cancellationToken);

        return new WorkspaceResponse(workspace.Id, workspace.Name, workspace.Description, workspace.CreatedAt);
    }
}
