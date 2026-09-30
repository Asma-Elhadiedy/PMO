
namespace PMO.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public EProjectStatus Status { get; set; } = EProjectStatus.Active;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate  { get; set; }


    /// <summary>
    /// Navigation Property
    /// </summary>

    public string OwnerId { get; set; } = default!;
    [ForeignKey(nameof(OwnerId))]

    /// <summary>
    /// Navigation Collection
    /// </summary>
    public virtual ICollection<ProjectTask>? Tasks { get; set; } = [];
}
