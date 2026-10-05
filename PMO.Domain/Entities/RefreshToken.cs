
namespace PMO.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = default!;
    public DateTime Expiration { get; set; }

    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; } = default!;
    public bool IsValid => DateTime.UtcNow < Expiration;
}