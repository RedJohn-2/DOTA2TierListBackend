using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Application.Interfaces;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;

namespace DOTA2TierList.Application.Services
{
    public class UserService
    {
        public readonly IUserStore _userStore;
        public readonly IPasswordHasher _passwordHasher;

        public UserService(IUserStore userStore, IPasswordHasher passwordHasher)
        {
            _userStore = userStore;
            _passwordHasher = passwordHasher;
        }

        public async Task Register(string name, string email, string password)
        {
            var existedUser = await _userStore.GetByEmail(email);

            if (existedUser != null)
            {
                throw new Exception();
            }

            var hash = _passwordHasher.Hash(password);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hash
            };

            await _userStore.Register(user);
        }

        public async Task<User> GetById(long id)
        {
            var user = await _userStore.GetById(id);

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userStore.GetByEmail(email);

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }
    }
}
