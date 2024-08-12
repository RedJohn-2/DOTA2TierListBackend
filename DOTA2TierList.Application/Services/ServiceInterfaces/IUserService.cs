using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task Register(User user);
        Task<(string, string)> Login(User user);
        string SteamAuth(string returnUrl);
        string GetVerifyAuthUrl(string queryParams);
        bool IsSuccessSteamAuth(string responseJson);
        Task<(string, string)> SteamLogin(string claimedIdUrl);
        Task<(string, string)> Refresh(string accessToken, string refreshToken);
        Task<User> GetById(long id);
        Task<User> GetByEmail(string email);
        Task Update(long userId, string? name, string? email);

    }
}
