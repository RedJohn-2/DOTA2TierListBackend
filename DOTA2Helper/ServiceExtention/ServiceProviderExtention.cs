using DOTA2TierList.Application.Services;
using DOTA2TierList.Persistence.Repository;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Infrastructure.Auth;

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

    }
}
