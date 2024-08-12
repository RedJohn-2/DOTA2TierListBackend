using DOTA2TierList.Logic.Models.Enums;
using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Services.ServiceInterfaces
{
    public interface IUserRolesService
    {
        void AddRoles(User user, params RoleEnum[] roles);
        void DeleteRoles(User user, params RoleEnum[] roles);
    }
}
