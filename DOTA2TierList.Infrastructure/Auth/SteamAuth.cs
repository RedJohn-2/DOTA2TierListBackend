using DOTA2TierList.Application.Interfaces.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class SteamAuth : ISteamAuth
    {
        private readonly SteamAuthOptions _steamAuthOptions;
        private const string SteamAuthBaseUrl = "https://steamcommunity.com/openid/login";
        public SteamAuth(IOptions<SteamAuthOptions> options)
        {
            _steamAuthOptions = options.Value;
        }

        public string GetSteamAuthURL(string returnUrl)
        {
            var realm = GetBaseAddress(new Uri(returnUrl));

            var url = SteamAuthBaseUrl +
                $"?openid.claimed_id={HttpUtility.UrlEncode(_steamAuthOptions.ClaimedId)}" +
                $"&openid.identity={HttpUtility.UrlEncode(_steamAuthOptions.Identity)}" +
                $"&openid.mode={HttpUtility.UrlEncode(_steamAuthOptions.Mode)}" +
                $"&openid.ns={HttpUtility.UrlEncode(_steamAuthOptions.Ns)}" +
                $"&openid.realm={HttpUtility.UrlEncode(realm)}" +
                $"&openid.return_to={HttpUtility.UrlEncode(returnUrl)}";

            return url;
        }

        private string GetBaseAddress(Uri uri)
        {
            string scheme = uri.Scheme;
            string host = uri.Host;
            string port = uri.IsDefaultPort ? "" : $":{uri.Port}";

            return $"{scheme}://{host}{port}";
        }
    }
}
