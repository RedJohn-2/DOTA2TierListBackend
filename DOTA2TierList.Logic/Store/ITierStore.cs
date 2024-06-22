using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Store
{
    public interface ITierStore
    {
        Task Add(Tier tier);

        Task<Tier> GetById(long id);

        Task<IReadOnlyList<Tier>> GetByName(string name);

        Task<IReadOnlyList<Tier>> GetByTierList(TierList tierList);
    }
}
