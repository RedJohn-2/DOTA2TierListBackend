using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DOTA2TierList.Application.Exceptions;
using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using FluentValidation;

namespace DOTA2TierList.Application.Services
{
    public class UserService
    {
        private readonly IUserStore _userStore;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(
            IUserStore userStore, 
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _userStore = userStore;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
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

        public async Task<string> Login(User user)
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

            var token = _jwtProvider.GenerateToken(user);

            return token;
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
    }
}
