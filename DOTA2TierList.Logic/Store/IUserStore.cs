using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Store
{
    public interface IUserStore
    {
        Task Create(User user);

        Task<User?> GetById(long id);

        Task<User?> GetBySteamId(long id);

        Task<IReadOnlyList<User>> GetAll();

        Task<IReadOnlyList<User>> GetByPage(int page, int pageSize);

        Task<IReadOnlyList<User>> GetByPageFilter(int page, int pageSize, UserFilter filter);

        Task<User?> GetByEmail(string email);

        Task UpdateData(long userId, string? name = null, string? email = null, string? passwordHash = null);

        Task UpdateRefreshToken(long userId, string? token, DateTime? tokenExpires);

        Task AddRole(long userId, RoleEnum role);

        Task DeleteRole(long userId, RoleEnum role);

        Task<IReadOnlyList<Role>> GetRoles(long userId);
    }
}
