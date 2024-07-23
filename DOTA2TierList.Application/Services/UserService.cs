using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DOTA2TierList.Application.Exceptions;
using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using DOTA2TierList.Logic.Store;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace DOTA2TierList.Application.Services
{
    public class UserService
    {
        private readonly IUserStore _userStore;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly ISteamAuth _steamAuth;

        public UserService(
            IUserStore userStore, 
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            ISteamAuth steamAuth)
        {
            _userStore = userStore;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _steamAuth = steamAuth;
        }

        public async Task Register(User user)
        {
           
            var existedUser = await _userStore.GetByEmail(user.Email);

            if (existedUser != null)
            {
                throw new UserDuplicateException();
            }
            
            user.PasswordHash = _passwordHasher.Hash(user.Password);

            await _userStore.Create(user);
        }

        public async Task<(string, string)> Login(User user)
        {

            var existedUser = await _userStore.GetByEmail(user.Email);

            if (existedUser == null)
            {
                throw new AuthenticationException();
            }


            if (!_passwordHasher.VerifyPassword(user.Password, existedUser.PasswordHash))
            {
                throw new AuthenticationException();
            }

            var accessToken = _jwtProvider.GenerateAccessToken(existedUser);

            var refreshToken = _jwtProvider.GenerateRefreshToken(existedUser);

            await _userStore.UpdateRefreshToken(existedUser.Id, existedUser.RefreshToken, existedUser.RefreshTokenExpires);

            return (accessToken, refreshToken);
        }

        public string SteamAuth(string returnUrl)
        {
            return _steamAuth.GetSteamAuthURL(returnUrl);
        }


        public async Task<(string, string)> Refresh(string accessToken, string refreshToken)
        {

            var userId = await _jwtProvider.GetUserIdFromExpiredToken(accessToken);

            var user = await _userStore.GetById(userId);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpires < DateTime.UtcNow)
            {
                throw new AuthenticationException();
            }

            var newAccessToken = _jwtProvider.GenerateAccessToken(user);

            var newRefreshToken = _jwtProvider.GenerateRefreshToken(user);

            await _userStore.UpdateRefreshToken(user.Id, user.RefreshToken, user.RefreshTokenExpires);

            return (newAccessToken, newRefreshToken);
        }

        public async Task<User> GetById(long id)
        {
            var user = await _userStore.GetById(id);

            if (user == null)
            {
                throw new UserNotFoundException("Not found user by this id");
            }

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userStore.GetByEmail(email);

            if (user == null)
            {
               throw new UserNotFoundException("Not found user with this Email address");
            }

            return user;
        }

        public async Task Update(long userId, string? name, string? email)
        {
            var user = await _userStore.GetById(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (email != null)
            {
                user = await _userStore.GetByEmail(email);

                if (user != null) 
                {
                    throw new UserDuplicateException();
                }
            }

            await _userStore.UpdateData(userId: userId, name: name, email: email);
    
        }

    }
}
