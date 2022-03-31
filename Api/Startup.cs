using ConstruFindAPI.API.Configuration;
using ConstruFindAPI.Configuration;
using Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConstruFindAPI.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Identity
            services.AddIdentityConfiguration(Configuration);

            //Api
            services.AddApiConfiguration();

            //Dependency Injection
            services.RegisterDI();

            //Swagger
            services.AddSwaggerConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AdminUserSeed adminUserSeed)
        {
            //Swagger
            app.UseSwaggerConfiguration();

            //Api + Identity
            app.UseApiConfiguration(env);

            //Seed
            adminUserSeed.SeedAdminUser();
        }
    }
}
