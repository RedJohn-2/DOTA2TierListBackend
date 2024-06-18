using DOTA2TierList.Logic.Models.TierListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    public class Artifact : TierListItem
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
