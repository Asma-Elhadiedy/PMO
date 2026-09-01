
namespace PMO.Domain.Entities;

public class ProjectTask : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ETaskStatus Status { get; set; }

    /// <summary>
    /// Navigation property
    /// </summary>
    public Guid ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public virtual Project? Project { get; set; }

    /// <summary>
    /// Navigation Collection
    /// </summary>
    public virtual ICollection<Comment>? Comments { get; set; } = [];
}
