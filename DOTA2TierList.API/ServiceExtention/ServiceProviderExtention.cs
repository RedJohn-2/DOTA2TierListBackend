using DOTA2TierList.Application.Services;
using DOTA2TierList.Persistence.Repository;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace DOTA2TierList.API.ServiceExtentions
{
    public static class ServiceProviderExtention
    {
        public static void AddUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserStore, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<UserService>();
        }

        public static void AddAuthenticationServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var jwtOpt = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            jwtOpt!.SecretKey
                            ))
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[jwtOpt.CookieKey];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }

    }
}
