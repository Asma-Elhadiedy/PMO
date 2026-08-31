

namespace PMO.Infrastructure.UoW;

public class UnitOfWork(AppDbContext _context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<Type, object> _repositoryDictionary = new();
    
    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    => (IGenericRepository<T>)_repositoryDictionary.GetOrAdd(
        typeof(T),
        _ => new GenericRepository<T>(_context));

    public async Task<int> CompleteAsync()
    {
        var updatedEntry = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity)
            .ToList();

        var date = DateTime.Now;
        foreach (var entry in updatedEntry)
        {
            ((BaseEntity)entry.Entity).UpdatedAt = date;
        }

        return await _context.SaveChangesAsync();
    }
    
    public async Task<bool> ExecuteTransactionAsync<T>(Func<Task<bool>> action, CancellationToken ct = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            var result = await action();
            if (result)
                await transaction.CommitAsync(ct);
            else
                await transaction.RollbackAsync(ct);
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}

