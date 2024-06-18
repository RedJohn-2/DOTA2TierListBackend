using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models.TierListModel
{
    public class Tier
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long TierListId { get; set; }
        public TierList TierList { get; set; } = null!;
        public List<TierListItem> listItems { get; set; } = new();
    }
}
