
namespace PMO.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string CreatedById { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
