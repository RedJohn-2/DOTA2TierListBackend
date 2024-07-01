using DOTA2TierList.Logic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models.TierItemTypes
{
    public static class TierItemFactory
    {
        public static TierItem CreateTierItem(TierListTypeEnum tierListType, int id)
        {
            switch (tierListType)
            {
                case TierListTypeEnum.HeroesTierList:
                    return new Hero { Id = id};
                case TierListTypeEnum.ArtifactTierList:
                    return new Artifact { Id = id};
                default:
                    throw new Exception();
            }
        }
    }
}
