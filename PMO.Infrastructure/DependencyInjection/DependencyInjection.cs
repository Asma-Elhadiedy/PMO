

namespace PMO.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureServices(string connectionString)
        {

            services.AddScoped<ISeedData, SeedData>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));
            
            return services;
        }
    }
}
