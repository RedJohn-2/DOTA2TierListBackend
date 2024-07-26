using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Entities
{
    public class SteamProfileEntity
    {
        public long Id { get; set; }

        public int MatchMakingRating { get; set; }

        public UserEntity User { get; set; } = null!;
    }
}
