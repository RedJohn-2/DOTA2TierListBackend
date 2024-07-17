using AutoMapper;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Persistence.Entities;
using DOTA2TierList.Persistence.Entities.TierItemTypes;
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
            await FillTiersWithItems(tierListEntity.Tiers!);

            await _db.TierLists.AddAsync(tierListEntity);
            await _db.SaveChangesAsync();
        }

        private async Task FillTiersWithItems(List<TierEntity> tiers)
        {
            var itemsIds = tiers.SelectMany(tier => tier.Items!.Select(item => item.Id)).ToList();
            var itemsEntity = await _db.TierItems.Where(item => itemsIds.Contains(item.Id)).ToArrayAsync();

            foreach (var tier in tiers)
            {
                tier.Items = itemsEntity.Where(item => tier.Items!.Select(ti => ti.Id).Contains(item.Id)).ToList();
            }
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
                .Include(tl => tl.Type)
                .FirstOrDefaultAsync();
            
            var tierList = _mapper.Map<TierList>(tierListEntity);

            return tierList;

        }

        public async Task<TierList> GetOnlyTierListById(long id)
        {
            var tierListEntity = await _db.TierLists.Where(tl => tl.Id == id)
                .FirstOrDefaultAsync();

            var tierList = _mapper.Map<TierList>(tierListEntity);

            return tierList;
        }

        public async Task<IReadOnlyList<TierList>> GetByPageFilter(int page, int pageSize, TierListFilter filter)
        {
            var query = _db.TierLists.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(tl => tl.Name == filter.Name);
            }

            if (filter.ModifiedDate is not null)
            {
                query = query.Where(tl => tl.ModifiedDate >= filter.ModifiedDate);
            }

            if (filter.Type is not null)
            {
                query = query.Where(tl => tl.TypeId == (int)filter.Type);
            }

            if (filter.UserId is not null)
            {
                query = query.Where(tl => tl.UserId == filter.UserId);
            }

            var tierListEntities = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            var tierLists = _mapper.Map<IReadOnlyList<TierList>>(tierListEntities);

            return tierLists;
        }

        public async Task Update(TierList tierList)
        {
            var updatedTierListEntity = _mapper.Map<TierListEntity>(tierList);

            var existingTierListEntity = await _db.TierLists.Include(tl => tl.Tiers)
                .FirstOrDefaultAsync(tl => tl.Id == tierList.Id);
            var countExistedTiers = existingTierListEntity!.Tiers!.Count;
            var countUpdatedTiers = updatedTierListEntity.Tiers!.Count;

            if (countUpdatedTiers > countExistedTiers)
            {
                var newTiers = updatedTierListEntity.Tiers.Skip(countExistedTiers).ToList();
                existingTierListEntity.Tiers.AddRange(newTiers);
            }
            else if (countUpdatedTiers < countExistedTiers)
            {
                existingTierListEntity.Tiers
                    .RemoveRange(countUpdatedTiers, countExistedTiers - countUpdatedTiers);
            }

            for (int i = 0; i < countUpdatedTiers; i++) 
            {
                existingTierListEntity.Tiers[i].Name = updatedTierListEntity.Tiers[i].Name;
                existingTierListEntity.Tiers[i].Description = updatedTierListEntity.Tiers[i].Description;
                existingTierListEntity.Tiers[i].Items = updatedTierListEntity.Tiers[i].Items;
            }

            await FillTiersWithItems(existingTierListEntity.Tiers);

            existingTierListEntity.Name = updatedTierListEntity.Name;
            existingTierListEntity.Description = updatedTierListEntity.Description;
            existingTierListEntity.TypeId = updatedTierListEntity.TypeId;
            existingTierListEntity.ModifiedDate = updatedTierListEntity.ModifiedDate;

            await _db.SaveChangesAsync();

        }
    }
}
