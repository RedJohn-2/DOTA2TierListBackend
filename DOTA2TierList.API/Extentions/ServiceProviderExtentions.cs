using DOTA2TierList.Application.Services;
using DOTA2TierList.Persistence.Repository;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using DOTA2TierList.Logic.Models.Enums;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using AngleSharp.Dom;
using static System.Net.WebRequestMethods;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Host.SystemWeb;
using AspNet.Security.OpenId;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DOTA2TierList.Application.Interfaces;
using DOTA2TierList.Infrastructure;

namespace DOTA2TierList.API.ServiceExtentions
{
    public static class ServiceProviderExtentions
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserStore, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<ISteamAuth,  SteamAuth>();
            services.AddScoped<ISteamApiProvider, SteamApiProvider>();
            services.AddScoped<UserRolesService>();
            services.AddScoped<UserService>();
            return services;
        }

        public static IServiceCollection AddTierListService(this IServiceCollection services)
        {
            services.AddScoped<ITierListStore, TierListRepository>();
            services.AddScoped<TierListService>();
            return services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var jwtOpt = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddSingleton<IAuthorizationHandler, UserRolesHandler>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt!.SecretKey))
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[jwtOpt.CookieAccessKey];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy =>
                {
                    policy.AddRequirements(new UserRolesRequirement((int)RoleEnum.Admin));   
                });

                opt.AddPolicy("User", policy =>
                {
                    policy.AddRequirements(new UserRolesRequirement((int)RoleEnum.User));
                });
            });

            return services;
        }

    }
}
