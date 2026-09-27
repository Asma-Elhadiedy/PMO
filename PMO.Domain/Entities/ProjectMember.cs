
namespace PMO.Domain.Entities;

public class ProjectMember : BaseEntity
{
    public Guid ProjectId { get; set; }
    public string UserId { get; set; }
    
    /// <summary>
    /// Navigation Property
    /// </summary>
    [ForeignKey(nameof(ProjectId))]
    public virtual Project? Project { get; set; }
}