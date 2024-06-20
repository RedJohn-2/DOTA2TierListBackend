using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using Microsoft.EntityFrameworkCore;

namespace DOTA2TierList.Persistence.Repository
{
    public class UserRepository : IUserStore
    {
        public readonly ApplicationContext _db;

        public UserRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task Register(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User?> GetById(long id)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;

        }

        public async Task<IReadOnlyList<User>> GetByName(string name)
        {
            var users = await _db.Users.Where(x => x.Name == name).ToListAsync();

            return users;
        }
    }
}
