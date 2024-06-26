using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Store
{
    public record TierListFilter(string? Name, TierListTypeEnum? Type, DateTime? ModifiedDate, User? user);
}
