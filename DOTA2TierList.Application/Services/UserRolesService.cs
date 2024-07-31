using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Services
{
    public class UserRolesService
    {
        public void AddRoles(User user, params RoleEnum[] roles)
        {
            user.Roles.AddRange(roles.Select(r => new Role()
            {
                Type = r,
                Name = r.ToString(),
            })
            .ToList());
        }

        public void DeleteRoles(User user, params RoleEnum[] roles)
        {
            user.Roles.RemoveAll(r => roles.Contains(r.Type));
        }
    }
}
