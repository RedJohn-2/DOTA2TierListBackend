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
        private const string DefualtNsString = "http://specs.openid.net/auth/2.0";

        public SteamAuth(IOptions<SteamAuthOptions> options)
        {
            _steamAuthOptions = options.Value;
        }

        public string GetSteamAuthUrl(string returnUrl)
        {
            var realm = GetBaseAddress(new Uri(returnUrl));

            var url = $"?openid.claimed_id={HttpUtility.UrlEncode(_steamAuthOptions.ClaimedId)}" +
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

        public string GetVerifyAuthQuery(string queryParams)
        {
            var parameters = HttpUtility.ParseQueryString(queryParams);
            parameters["openid.mode"] = "check_authentication";

            var url = $"{SteamAuthBaseUrl}?{parameters}"; 

            return url;
        }

        public bool IsSuccess(string response)
        {
            var lines = response.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var dictionary = new Dictionary<string, string>();

            foreach (var line in lines)
            {

                var parts = line.Split(':', 2);

                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    dictionary[key] = value;
                }
            }

            if (!dictionary.ContainsKey("ns") || dictionary["ns"] != DefualtNsString)
            {
                return false;
            }

            return dictionary.ContainsKey("is_valid") && bool.Parse(dictionary["is_valid"]);
        }
    }
}
