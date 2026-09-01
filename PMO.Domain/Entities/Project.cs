
namespace PMO.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public EProjectStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate  { get; set; }


    /// <summary>
    /// Navigation Collection
    /// </summary>
    public virtual ICollection<ProjectTask>? Tasks { get; set; } = [];
}
