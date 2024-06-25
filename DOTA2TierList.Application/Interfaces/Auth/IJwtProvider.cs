using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken(User user);

        Task<long> GetUserIdFromExpiredToken(string? token);
        

    }
}
