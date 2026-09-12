

namespace PMO.Application.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            var assembly = typeof(IApplicationMarker).Assembly;

            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            return services;
        }
    }
}
