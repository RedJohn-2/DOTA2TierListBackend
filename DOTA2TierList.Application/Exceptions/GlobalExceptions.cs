using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg) { }
    }

    public abstract class DuplicateException : Exception
    {
        public DuplicateException(string msg) : base(msg) { }
    }
}
