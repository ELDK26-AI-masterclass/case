namespace ApplyProjectStudio.Domain.Workspaces;

public sealed class InvalidWorkspaceNameException : Exception
{
    public InvalidWorkspaceNameException(string? name)
        : base($"Workspace name is invalid: '{name}'.")
    {
    }
}

public sealed class WorkspaceAlreadyExistsException : Exception
{
    public WorkspaceAlreadyExistsException(string name)
        : base($"Workspace already exists: '{name}'.")
    {
    }
}
