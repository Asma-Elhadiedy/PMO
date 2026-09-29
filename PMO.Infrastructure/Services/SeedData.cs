using PMO.Domain.Constants;

namespace PMO.Infrastructure.Services;

public class SeedData(AppDbContext _context, RoleManager<IdentityRole> _roleManager) : ISeedData
{
    public async Task SeedAsync()
    {
        await InitializeDatabase();
        await SeedRolesAsync();
    }

    async Task InitializeDatabase()
    {
        if (!_context.Database.HasPendingModelChanges())
            await _context.Database.MigrateAsync();
    }

    async Task SeedRolesAsync()
    {
        if (await _roleManager.Roles.AnyAsync())
            return;

        await _roleManager.CreateAsync(new IdentityRole { Name = ConstRoles.Admin });
        await _roleManager.CreateAsync(new IdentityRole { Name = ConstRoles.User });
    }

}
