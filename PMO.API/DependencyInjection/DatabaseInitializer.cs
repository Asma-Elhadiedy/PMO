
namespace PMO.API.DependencyInjection;

internal static class DatabaseInitializer
{
    extension(WebApplication app)
    {
        internal async Task InitializeDatabaseAsync()
        {
            using var scope = app.Services.CreateScope();
            var seedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await seedData.SeedAsync();
        }
    }
}
