using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;

namespace DOTA2TierList.Application.Services
{
    public class UserService
    {
        public readonly IUserStore _userStore;

        public UserService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task Create(User user)
        {
            var existedUser = await _userStore.GetByEmail(user.Email);

            if (existedUser != null)
            {
                throw new Exception();
            }

            await _userStore.Add(user);
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
