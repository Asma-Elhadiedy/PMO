

namespace PMO.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureServices(string connectionString)
        {

            services.AddScoped<ISeedData, SeedData>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddDbContext<AppDbContext>(o => 
                o.UseSqlServer(connectionString));
            
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<AppDbContext>();


            services.AddOptions<JWTTokenOptions>()
                .BindConfiguration(JWTTokenOptions.SectionName); 

            return services;
        }
    }
}
