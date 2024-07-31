using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    public class SteamProfile
    {
        public long Id { get; set; }
        public int MatchMakingRating { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
