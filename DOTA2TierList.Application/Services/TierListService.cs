using DOTA2TierList.Application.Exceptions;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Services
{
    public class TierListService
    {
        private readonly ITierListStore _tierListStore;

        public TierListService(ITierListStore tierListStore)
        {
            _tierListStore = tierListStore;
        }

        public async Task Add(TierList tierList)
        {
            tierList.ModifiedDate = DateTime.UtcNow;
            await _tierListStore.Add(tierList);
        }

        public async Task<TierList> GetById(long id)
        {
            var tierList = await _tierListStore.GetById(id);

            if (tierList is null)
            {
                throw new TierListNotFoundException();
            }

            return tierList;
        }

        public async Task Delete(long tierListId, long userId)
        {
            var exsistedTierList = await _tierListStore.GetOnlyTierListById(tierListId);

            if (exsistedTierList is null)
            {
                throw new TierListNotFoundException();
            }

            if (exsistedTierList.UserId != userId)
            {
                throw new ForbiddenException();
            }

            await _tierListStore.Delete(tierListId);
        }

        public async Task Update(TierList tierList, long userId)
        {
            var exsistedTierList = await _tierListStore.GetOnlyTierListById(tierList.Id);

            if (exsistedTierList is null)
            {
                throw new TierListNotFoundException();
            }

            if (exsistedTierList.UserId != userId)
            {
                throw new ForbiddenException();
            }

            tierList.ModifiedDate = DateTime.UtcNow;

            await _tierListStore.Update(tierList);
        }

        public async Task<IReadOnlyList<TierList>> GetByPageFilter(int page, int pageSize, TierListFilter filter)
        {
            var tierLists = await _tierListStore.GetByPageFilter(page, pageSize, filter);

            return tierLists;
        }
    }
}
