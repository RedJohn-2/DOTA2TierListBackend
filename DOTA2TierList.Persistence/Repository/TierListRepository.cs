using AutoMapper;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Repository
{
    public class TierListRepository : ITierListStore
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public TierListRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task Add(TierList tierList)
        {
            var tierListEntity = _mapper.Map<TierListEntity>(tierList);
            await _db.TierLists.AddAsync(tierListEntity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(long tierListId)
        {
            await _db.TierLists.Where(tl => tl.Id == tierListId)
                .ExecuteDeleteAsync();
        }

        public async Task<TierList> GetById(long id)
        {
            var tierListEntity = await _db.TierLists.Where(tl => tl.Id == id)
                .Include(tl => tl.Tiers)
                .Include(tl => tl.User)
                .FirstOrDefaultAsync();
            
            var tierList = _mapper.Map<TierList>(tierListEntity);

            return tierList;

        }

        public async Task<IReadOnlyList<TierList>> GetByPageFilter(int page, int pageSize, TierListFilter filter)
        {
            var query = _db.TierLists.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(u => u.Name == filter.Name);
            }

            if (filter.ModifiedDate is not null)
            {
                query = query.Where(u => u.ModifiedDate == filter.ModifiedDate);
            }

            if (filter.Type is not null)
            {
                query = query.Where(u => u.TypeId == (int)filter.Type);
            }

            if (filter.user is not null)
            {
                query = query.Where(u => u.UserId == filter.user.Id);
            }

            var tierListEntities = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            var tierLists = _mapper.Map<IReadOnlyList<TierList>>(tierListEntities);

            return tierLists;
        }

        public async Task<bool> IsExist(long id)
        {
            var tierListEntity = await _db.TierLists.Where(tl => tl.Id == id).FirstOrDefaultAsync();

            return tierListEntity is not null;
        }

        public async Task Update(TierList tierList)
        {
            var tierEntities = _mapper.Map<IReadOnlyList<TierEntity>>(tierList.Tiers);

            await _db.TierLists.Where(tl => tl.Id == tierList.Id)
                .ExecuteUpdateAsync(tl => tl
                .SetProperty(tl => tl.Name, tierList.Name)
                .SetProperty(tl => tl.Description, tierList.Description)
                .SetProperty(tl => tl.ModifiedDate, tierList.ModifiedDate)
                .SetProperty(tl => tl.Tiers, tierEntities)
                );
        }
    }
}
