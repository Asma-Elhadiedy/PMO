

namespace PMO.Infrastructure.Services;

public class SeedData(AppDbContext _context) : ISeedData
{
    public async Task SeedAsync()
    {
        await InitializeDatabase();
    }

    async Task InitializeDatabase()
    {
        if (!_context.Database.HasPendingModelChanges())        
            await _context.Database.MigrateAsync();   
    }

}
