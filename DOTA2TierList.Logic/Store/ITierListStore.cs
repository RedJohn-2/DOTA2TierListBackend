using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;
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

        Task<TierList> GetById(long id);

        Task<IReadOnlyList<TierList>> GetByName(string name);

        Task<IReadOnlyList<TierList>> GetByUser(User user);
    }
}
