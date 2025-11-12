namespace KF.Host.Settings;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSettings(IConfiguration configuration)
        {
            services.AddOptions<SubscriptionSettings>()
                .Bind(configuration.GetSection("Subscription"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
    }
}