using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Services.ServiceInterfaces
{
    public interface ITierListService
    {
        Task Add(TierList tierList);
        Task<TierList> GetById(long id);
        Task Delete(long tierListId, long userId);
        Task Update(TierList tierList, long userId);
        Task<IReadOnlyList<TierList>> GetByPageFilter(int page, int pageSize, TierListFilter filter);

    }
}
