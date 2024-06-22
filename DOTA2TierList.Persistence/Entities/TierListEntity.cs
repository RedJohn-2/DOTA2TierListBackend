using DOTA2TierList.Logic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Entities
{
    public class TierListEntity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int TypeId { get; set; }

        public TierListTypeEntity Type { get; set; } = new();

        public long UserId { get; set; }
        public UserEntity User { get; set; } = new();

        public List<TierEntity> Tiers { get; set; } = [];
    }
}
