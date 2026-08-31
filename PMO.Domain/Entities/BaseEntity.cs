
namespace PMO.Domain.Entities;

public class BaseEntity
{
    public string Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
