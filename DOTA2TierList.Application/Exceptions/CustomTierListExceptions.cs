using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Exceptions
{
    public class TierListNotFoundException : NotFoundException
    { public TierListNotFoundException(string msg = "TierList not found!") : base(msg) { } }
}
