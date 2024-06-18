using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Logic.Models.TierListModel;

namespace DOTA2TierList.Logic.Models
{
    public class Hero : TierListItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
