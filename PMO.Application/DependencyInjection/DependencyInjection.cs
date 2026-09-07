

namespace PMO.Application.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            var assembly = typeof(IApplicationMarker).Assembly;

            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
            return services;
        }
    }
}
