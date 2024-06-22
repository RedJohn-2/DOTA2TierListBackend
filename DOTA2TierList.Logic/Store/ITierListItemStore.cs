using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Store
{
    public interface ITierListItemStore
    {
        Task Add(TierItem item);

        Task<TierItem> GetById(long id);

        Task<IReadOnlyList<TierItem>> GetByName(string name);

        Task<IReadOnlyList<TierItem>> GetByTierList(TierList tierList);

        Task<IReadOnlyList<TierItem>> GetByTier(Tier tier);

    }
}
