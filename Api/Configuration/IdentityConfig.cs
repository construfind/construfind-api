using ConstruFindAPI.API.Configuration.Extensions;
using ConstruFindAPI.Business.Models;
using ConstruFindAPI.Data.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConstruFindAPI.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //Context
            services.AddDbContext<ConstrufindContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.TryAddSingleton<ISystemClock, SystemClock>();

            //Identity support
            services.AddIdentity<Usuario, IdentityRole>(p => 
            {
                p.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ConstrufindContext>()
            .AddErrorDescriber<IdentityMessagesTranslate>()
            .AddDefaultTokenProviders();

            //AppSettings
            var appSettingsSection = configuration.GetSection("JWTConfig");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            //Auth JWT
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOpt =>
            {
                bearerOpt.RequireHttpsMetadata = true;
                bearerOpt.SaveToken = true;
                bearerOpt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValideOn,
                    ValidIssuer = appSettings.Issuer
                };
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthorization();

            app.UseAuthentication();

            return app;
        }
    }
}
