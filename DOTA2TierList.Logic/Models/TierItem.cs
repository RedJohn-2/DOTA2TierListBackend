﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    public abstract class TierItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Tier> Tiers { get; set; } = new();
    }
}
