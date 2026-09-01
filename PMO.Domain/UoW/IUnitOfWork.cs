

namespace PMO.Domain.UoW;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    Task<bool> ExecuteTransactionAsync<T>(Func<Task<bool>> action, CancellationToken ct = default);
    Task<int> CompleteAsync(CancellationToken ct = default);
}
