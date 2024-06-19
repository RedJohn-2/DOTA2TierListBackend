using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models.TierListModel
{
    public abstract class TierListItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Tier> tiers { get; set; } = new();
    }
}
