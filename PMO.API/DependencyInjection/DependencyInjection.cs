
namespace PMO.API.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentationServices(string connectionString)
        {
            services
                .AddApplicationServices()
                .AddInfrastructureServices(connectionString);

            services.AddSwaggerGen();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
