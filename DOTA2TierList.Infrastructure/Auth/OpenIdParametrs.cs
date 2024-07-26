using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class OpenIdParameters
    {
        public string Ns { get; set; } = null!;
        public string Mode { get; set; } = null!;
        public string OpEndpoint { get; set; } = null!;
        public string ClaimedId { get; set; } = null!;
        public string Identity { get; set; } = null!;
        public string ReturnTo { get; set; } = null!;
        public string ResponseNonce { get; set; } = null!;
        public string AssocHandle { get; set; } = null!;
        public string Signed { get; set; } = null!;
        public string Sig { get; set; } = null!;
    }
}
