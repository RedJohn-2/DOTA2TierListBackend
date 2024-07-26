using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Interfaces.Auth
{
    public interface ISteamAuth
    {
        string GetSteamAuthUrl(string returnUrl);

        string GetVerifyAuthQuery(string queryParams);

        bool IsSuccess(string response);
    }
}
