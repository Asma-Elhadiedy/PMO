
namespace PMO.Infrastructure.Repository;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity
{

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
         => await _context.Set<T>().FindAsync([id], cancellationToken: ct);
    public async Task<T1?> GetItemSelectedAsync<T1>(Expression<Func<T, T1>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
    {
        IQueryable<T> query = _context.Set<T>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.Select(selector).FirstOrDefaultAsync(ct);
    }
    public async Task<IReadOnlyList<T1>> GetAllSelectedAsync<T1>(Expression<Func<T, T1>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
    {
        IQueryable<T> query = _context.Set<T>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.Select(selector).ToListAsync(ct);
    }
    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Set<T>().AnyAsync(e => e.Id == id, ct);
    }

    public bool Add(T entity)
        => _context.Set<T>().Add(entity) != null;

    public bool Remove(T entity, CancellationToken ct = default)
    {
        _context.Set<T>().Remove(entity);
        return true;
    }

    public async Task<int> BulkDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => await _context.Set<T>().Where(predicate).ExecuteDeleteAsync(ct);

}
