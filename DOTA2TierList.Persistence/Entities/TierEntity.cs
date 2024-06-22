using DOTA2TierList.Logic.Models.TierListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Entities
{
    public class TierEntity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public long TierListId { get; set; }

        public TierListEntity TierList { get; set; } = new();

        public List<TierItemEntity> Items { get; set; } = [];
    }
}
