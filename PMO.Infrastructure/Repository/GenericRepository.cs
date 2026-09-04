
namespace PMO.Infrastructure.Repository;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity
{

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
         => await _context.Set<T>().FindAsync([id], cancellationToken: ct);

    public async Task<IReadOnlyList<T1>> GetAllSelectedAsync<T1>(Expression<Func<T, T1>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default) where T1 : class
    {
        IQueryable<T> query = _context.Set<T>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.Select(selector).ToListAsync(ct);
    }

    public bool Add(T entity)
        => _context.Set<T>().Add(entity) != null;

}
