
namespace PMO.Infrastructure.Persistence.Configurations;

internal class CommentsConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(c => c.Task)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.TaskId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
