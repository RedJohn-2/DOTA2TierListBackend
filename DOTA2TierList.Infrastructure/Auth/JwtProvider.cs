using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Logic.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        public JwtProvider(IOptions<JwtOptions> options) 
        { 
            _jwtOptions = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            List<Claim> claims = [new("userId", user.Id.ToString()),];
            claims.AddRange(user.Roles.Select(r => new Claim("Roles", ((int)r.Type).ToString())).ToList());


            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddSeconds(_jwtOptions.ExpiresAccessTokenSeconds)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpires = DateTime.UtcNow.AddSeconds(_jwtOptions.ExpiresRefreshTokenSeconds);

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string?> GetUserIdFromExpiredToken(string? token)
        {
            
            var validation = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions!.SecretKey))
            };
            
            var validationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, validation);

            return validationResult.Claims["userId"].ToString();
        }

    }
}
