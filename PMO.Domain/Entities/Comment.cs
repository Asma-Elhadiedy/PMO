
namespace PMO.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = default!;

    /// <summary>
    /// Navigation property
    /// </summary>
    public Guid TaskId { get; set; }
    [ForeignKey(nameof(TaskId))]
    public virtual ProjectTask? Task { get; set; }
}
