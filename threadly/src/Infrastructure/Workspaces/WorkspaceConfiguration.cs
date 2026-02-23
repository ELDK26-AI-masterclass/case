using ApplyProjectStudio.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyProjectStudio.Infrastructure.Workspaces;

public sealed class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.ToTable("workspaces");

        builder.HasKey(workspace => workspace.Id);

        builder.Property(workspace => workspace.Id)
            .HasColumnName("id");

        builder.Property(workspace => workspace.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(workspace => workspace.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        builder.Property(workspace => workspace.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasIndex(workspace => workspace.Name)
            .IsUnique();
    }
}
