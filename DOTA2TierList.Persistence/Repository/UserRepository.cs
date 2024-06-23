using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOTA2TierList.Persistence.Repository
{
    public class UserRepository : IUserStore
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task Create(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            await _db.Users.AddAsync(userEntity);
            await _db.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            UserEntity? userEntity = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            var user = _mapper.Map<User>(userEntity);
            return user;
        }

        public async Task<User?> GetById(long id)
        {
            UserEntity? userEntity = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var user = _mapper.Map<User>(userEntity);
            return user;

        }

        public async Task<IReadOnlyList<User>> GetByName(string name)
        {
            var userEntities = await _db.Users.Where(x => x.Name == name).ToListAsync();
            var users = _mapper.Map<List<User>>(userEntities);
            return users;
        }

        public Task<User?> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
