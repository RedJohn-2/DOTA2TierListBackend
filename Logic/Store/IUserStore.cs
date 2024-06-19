using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Store
{
    public interface IUserStore
    {
        Task Add(User user);

        Task<User?> GetById(long id);

        Task<IReadOnlyList<User>> GetByName(string name);

        Task<User?> GetByEmail(string email);

    }
}
