

namespace PMO.Domain.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    
    Task<IReadOnlyList<T1>> GetAllSelectedAsync<T1>(Expression<Func<T, T1>> selector, Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default) where T1 : class;
    
    bool Add(T entity);
}
