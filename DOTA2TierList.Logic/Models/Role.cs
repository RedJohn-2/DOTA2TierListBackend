using DOTA2TierList.Logic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    public class Role
    {
        public RoleEnum Type { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
