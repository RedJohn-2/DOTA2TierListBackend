using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Interfaces.DbInterfaces
{
    public static class UnitOfWorkExtentions
    {
        public static async Task InTransaction(this IUnitOfWork unitOfWork, Func<Task> action)
        {
            await unitOfWork.BeginTransactionAsync();

            try
            {
                await action();

                await unitOfWork.CommitAsync();
            }
            catch
            {
                await unitOfWork.RollbackAsync();
            }

        }
    }
}
