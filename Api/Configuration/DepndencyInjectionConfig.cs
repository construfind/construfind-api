using Data.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace ConstruFindAPI.Configuration
{
    public static class DepndencyInjectionConfig
    {
        public static IServiceCollection RegisterDI(this IServiceCollection services)
        {
            RegisterSeeds(services);

            return services;
        }

        private static void RegisterSeeds(IServiceCollection services)
        {
            services.AddTransient<AdminUserSeed>();
        }
    }
}
