using DOTA2TierList.API.Contracts;
using DOTA2TierList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Mappers
{
    public class UserMapper
    {
        public UserResponse ToUserResponse(User user)
        {
            return new UserResponse(user.Name, user.Email);
        }
    }
}
