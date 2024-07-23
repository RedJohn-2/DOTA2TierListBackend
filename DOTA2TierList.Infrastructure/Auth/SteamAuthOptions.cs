using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class SteamAuthOptions
    {
        public string ClaimedId { get; set; } = string.Empty;
        public string Identity { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string Ns { get; set; } = string.Empty;
    }
}
