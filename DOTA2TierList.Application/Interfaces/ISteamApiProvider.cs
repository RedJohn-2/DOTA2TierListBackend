using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Interfaces
{
    public interface ISteamApiProvider
    {
        long GetSteamId(string claimedIdUrl);

        Task<SteamUserInfo> GetDotaUserInfo(long steamId);
    }
}
