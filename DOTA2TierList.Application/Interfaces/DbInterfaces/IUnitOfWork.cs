﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Interfaces.DbInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

    }
}
