using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    public record SteamUserInfo
    {
        public string Name { get; init; } = string.Empty;

        public int MatchMakingRating { get; init; }

    }
}
