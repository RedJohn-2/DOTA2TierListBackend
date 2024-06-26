using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
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

        public async Task AddRole(long userId, RoleEnum role)
        {
            var userEntity = await _db.Users.Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var roleEntity = await _db.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == (int)role)
                ?? throw new Exception();

            userEntity!.Roles.Add(roleEntity!);

            await _db.SaveChangesAsync();
        }

        public async Task Create(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == (int)RoleEnum.User);

            userEntity.Roles.Add(role!);

            await _db.Users.AddAsync(userEntity);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteRole(long userId, RoleEnum role)
        {
            var userEntity = await _db.Users.Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var roleEntity = await _db.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == (int)role);

            userEntity!.Roles.Remove(roleEntity!);

            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<User>> GetAll()
        {
            var usersEntity = await _db.Users.AsNoTracking().ToListAsync();

            var users = _mapper.Map<IReadOnlyList<User>>(usersEntity);

            return users;
        }

        public async Task<User?> GetByEmail(string email)
        {
            UserEntity? userEntity = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            var user = _mapper.Map<User>(userEntity);
            return user;
        }

        public async Task<User?> GetById(long id)
        {
            UserEntity? userEntity = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var user = _mapper.Map<User>(userEntity);
            return user;

        }

        public async Task<IReadOnlyList<User>> GetByName(string name)
        {
            var userEntities = await _db.Users.Where(x => x.Name == name).ToListAsync();
            var users = _mapper.Map<List<User>>(userEntities);
            return users;
        }

        public async Task<IReadOnlyList<User>> GetByPage(int page, int pageSize)
        {
            var usersEntity = await _db.Users.AsNoTracking()
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            var users = _mapper.Map<IReadOnlyList<User>>(usersEntity);

            return users;
        }

        public async Task<IReadOnlyList<User>> GetByPageFilter(int page, int pageSize, UserFilter filter)
        {
            var query = _db.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(u => u.Name == filter.Name);
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(u => u.Email == filter.Email);
            }


            var userEntities = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            var users = _mapper.Map<IReadOnlyList<User>>(userEntities);

            return users;
        }

        public async Task<IReadOnlyList<Role>> GetRoles(long userId)
        {
            var usersEntity = await _db.Users.AsNoTracking().Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);


            var roles = _mapper.Map<IReadOnlyList<Role>>(usersEntity!.Roles);

            return roles;
        }

        public async Task UpdateData(long userId, string? name = null, string? email = null, string? passwordHash = null)
        {
            await _db.Users.Where(u => u.Id == userId)
                .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Name, u => name ?? u.Name)
                .SetProperty(u => u.Email, u => email ?? u.Email)
                .SetProperty(u => u.PasswordHash, u => passwordHash ?? u.PasswordHash)
                );
        }

        public async Task UpdateRefreshToken(long userId, string? token, DateTime? tokenExpires)
        {
            await _db.Users.Where(u => u.Id == userId)
                .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.RefreshToken, token)
                .SetProperty(u => u.RefreshTokenExpires, tokenExpires)
                );
        }
    }
}
