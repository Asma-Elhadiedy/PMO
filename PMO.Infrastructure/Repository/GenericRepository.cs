
namespace PMO.Infrastructure.Repository;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
         => await _context.Set<T>().FindAsync(id, ct);

}
