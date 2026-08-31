
namespace PMO.API.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAppServices(string connectionString)
        {
            services.AddInfrastructureServices(connectionString);
            return services;
        }
    }
}
