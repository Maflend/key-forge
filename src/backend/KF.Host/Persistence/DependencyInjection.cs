using Microsoft.EntityFrameworkCore;

namespace KF.Host.Persistence;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistence(IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            
            return services;
        }
    }
}