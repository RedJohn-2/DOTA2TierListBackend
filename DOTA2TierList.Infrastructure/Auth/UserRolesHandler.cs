using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Infrastructure.Auth
{
    public class UserRolesHandler : AuthorizationHandler<UserRolesRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            UserRolesRequirement requirement)
        {
            var rolesClaim = context.User.FindAll(u => u.Type == "Roles");

            if (rolesClaim is not null)
            {
                var rolesId = rolesClaim.Select(r => int.Parse(r.Value)).ToList();

                if (rolesId.Contains(requirement.Role))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
