using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Entities
{
    public class TierListTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<TierListEntity>? TierLists { get; set; }
    }
}
