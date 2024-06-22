using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class UserRolesRequirement : IAuthorizationRequirement
    {
        protected internal int Role {  get; set; }

        public UserRolesRequirement(int role)
        {
            Role = role;
        }
    }
}
