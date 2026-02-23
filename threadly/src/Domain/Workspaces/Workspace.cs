namespace ApplyProjectStudio.Domain.Workspaces;

public class Workspace
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private Workspace()
    {
        Name = string.Empty;
    }

    private Workspace(Guid id, string name, string? description, DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    public static Workspace Create(string name, string? description, DateTimeOffset createdAt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidWorkspaceNameException(name);
        }

        var trimmedName = name.Trim();
        var trimmedDescription = string.IsNullOrWhiteSpace(description) ? null : description.Trim();

        return new Workspace(Guid.NewGuid(), trimmedName, trimmedDescription, createdAt);
    }
}
