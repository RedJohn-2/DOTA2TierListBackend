﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    public class TierList
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public long UserId { get; set; }
        public User User { get; set; } = new();

        public TierListType Type { get; set; } = new();

        public List<Tier> Tiers { get; set; } = new();

    }
}
