using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;

        public int ExpiresAccessTokenSeconds { get; set; }

        public int ExpiresRefreshTokenSeconds { get; set; }

        public string CookieAccessKey {  get; set; } = string.Empty;

        public string CookieRefreshKey { get; set; } = string.Empty;
    }
}
