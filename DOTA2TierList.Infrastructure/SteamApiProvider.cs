using DOTA2TierList.Application.Exceptions;
using DOTA2TierList.Application.Interfaces;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Logic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure
{
    public class SteamApiProvider : ISteamApiProvider
    {

        private const string SteamApiPattern = @"https://steamcommunity\.com/openid/id/(\d+)";

        private const long SteamBaseId = 76561197960265728;

        private readonly string _steamApiKey;

        public SteamApiProvider(IConfiguration configuration)
        {
            _steamApiKey = configuration["SteamApiKey"]!;
        }

        public long GetSteamId(string claimedIdUrl)
        {
            Match match = Regex.Match(claimedIdUrl, SteamApiPattern);

            if (!match.Success)
            {
                throw new AuthenticationException();
            }

            string steamIdString = match.Groups[1].Value;

            return long.Parse(steamIdString);

        }

        public async Task<SteamUserInfo> GetDotaUserInfo(long steamId)
        {
            var steamId32bit = (int)(steamId - SteamBaseId);

            string url = $"https://api.opendota.com/api/players/{steamId32bit}";

            using var client = new HttpClient();

            (string personaname, int matchMakingRating) = await GetPersonaName(url, client);

            return new SteamUserInfo { Name = personaname, MatchMakingRating = matchMakingRating };
        }

        private async Task<(string, int)> GetPersonaName(string url, HttpClient client)
        {
            var result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                var data = JObject.Parse(content);

                return ((string)data["profile"]!["personaname"]!, (int)data["rank_tier"]!);

            }
            else
            {
                throw new Exception();
            }
        }
    }
}
