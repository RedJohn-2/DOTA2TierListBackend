using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Store
{
    public interface ITierListStore
    {
        Task Add(TierList tierList);

        Task Update(TierList tierList);

        Task Delete(long tierListId);

        Task<TierList> GetById(long id);

        Task<TierList> GetOnlyTierListById(long id);

        Task<IReadOnlyList<TierList>> GetByPageFilter(int page, int pageSize, TierListFilter filter);

    }
}
