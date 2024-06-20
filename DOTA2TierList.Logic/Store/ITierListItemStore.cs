using DOTA2TierList.Logic.Models.TierListModel;
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
        Task Add(TierListItem item);

        Task<TierListItem> GetById(long id);

        Task<IReadOnlyList<TierListItem>> GetByName(string name);

        Task<IReadOnlyList<TierListItem>> GetByTierList(TierList tierList);

        Task<IReadOnlyList<TierListItem>> GetByTier(Tier tier);

    }
}
