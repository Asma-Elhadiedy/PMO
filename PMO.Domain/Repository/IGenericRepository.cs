

namespace PMO.Domain.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<T1?> GetItemSelectedAsync<T1>(Expression<Func<T, T1>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

    Task<IReadOnlyList<T1>> GetAllSelectedAsync<T1>(Expression<Func<T, T1>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    bool Add(T entity);

    bool Remove(T entity, CancellationToken ct = default);

    Task<int> BulkDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

}
