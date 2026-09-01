
namespace PMO.Infrastructure.Persistence;

internal class TasksConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
