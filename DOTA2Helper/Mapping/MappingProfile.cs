using AutoMapper;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.Logic.Models;

namespace DOTA2TierList.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<RegisterUserRequest, User>();
            this.CreateMap<User, UserResponse>();
        }
    }
}
